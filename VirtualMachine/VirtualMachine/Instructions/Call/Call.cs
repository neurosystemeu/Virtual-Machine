using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using Mono.Cecil;
using NeuroSystem.VirtualMachine.Klasy;
using Dynamitey;
using NeuroSystem.VirtualMachine.Instrukcje.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Call : InstructionBase
    {
        public Call(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var mr = instrukcja.Operand as MethodReference;
            if (mr.FullName.Contains("VirtualMachine::Hibernate()"))
            {
                //wywołał metodę do hibernacji wirtualnej maszyny
                WirtualnaMaszyna.HibernujWirtualnaMaszyne();
                return;
            }
            var md = mr.Resolve();


            //pobieram argumenty ze stosu i kładę do zmiennych funkcji      
            int iloscArgumentow = md.Parameters.Count;
            object instancja = null;
            if (md.HasThis)
            {
                iloscArgumentow++;//+1 to zmienna this
               //pobieram wlasciwa instancje obiektu ktorego metode wykonamy
                instancja = PobierzElementZeStosu(iloscArgumentow - 1);
            }

            

            //sprawdzam czy getter lub seter lub czy metoda lub klasa oznaczona atrybutem Interpertuj
            if (CzyWykonacCzyInterpretowac(md) == true)
            {
                //wykonujemy
                WykonajMetode(md, instancja);
            }
            else
            {
                //interpretujemy
                var staraMetoda = WirtualnaMaszyna.AktualnaMetoda;

                var m = new WykonywanaMetoda();
                m.NazwaTypu = md.DeclaringType.FullName;
                m.NazwaMetody = md.Name;
                m.AssemblyName = md.DeclaringType.Module.FullyQualifiedName;
                m.NumerWykonywanejInstrukcji = 0;

                WirtualnaMaszyna.AktualnaMetoda = m;

                //pobieram argumenty ze stosu i kładę do zmiennych funkcji
                WczytajLokalneArgumenty(iloscArgumentow);

                //zapisuję aktualną metodę na stosie
                Push(staraMetoda);
                WykonajNastepnaInstrukcje();
            }            
        }

        public void WykonajMetode(MethodReference mr, object instancja)
        {
            var md = mr.Resolve();
            var gm = mr as Mono.Cecil.GenericInstanceMethod;

            //wykonujemy
            if (md != null && md.IsGetter == true)
            {
                Type typ;
                //wykonuje gettera - 

                //jeśli instancja jest adresem do obiektu
                var adres = instancja as VariableAddressBase;
                if (adres != null)
                {
                    instancja = adres.GetValue();
                }

                //getter może mieć parametr - jeśli jest operatore [parametry]
                int iloscArgumentow2 = mr.Parameters.Count;
                var arg = new List<object>();
                for (int i = 0; i < iloscArgumentow2; i++)
                {
                    var o = Pop();
                    arg.Add(o);
                }


                //!! z jakiegoś powodu to tu było, ale jak mamy getter z instancją
                //to instancję pobieramy w funkcji powyżej i drugie pobranie psuje stos
                if (md.HasThis)
                {
                    //Wykonujemy gettera
                    instancja = Pop();
                }
                typ = md.DeclaringType.GetSystemType();
               

                arg.Reverse();
                var argumenty = arg.ToArray();
                var propertyInfo = typ.GetProperty(md.Name.Replace("get_", ""));
                object wynik = null;

                if (typ.IsGenericType)
                {

                    var typNulleblowany = instancja.GetType();
                    var typNulleble = typ.MakeGenericType(typNulleblowany);
                    var pi = typNulleble.GetProperty(md.Name.Replace("get_", ""));
                    wynik = pi.GetValue(instancja, argumenty);
                }
                else
                {
                    wynik = propertyInfo.GetValue(instancja, argumenty);
                }

                if (wynik == null)
                {
                    Push(wynik);
                } else
                {
                    //sprawdzam czy zwracany tym jest Nullable<typ>
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.Name.Contains("Nullable"))
                    {
                        //jest nulable, więc wynik muszę opakować w nulable
                        var typWyniku = wynik.GetType();
                        if (typWyniku.IsValueType)
                        {
                            var typNullable = typeof(Nullable<>).MakeGenericType(typWyniku);
                            var wynikNullable = Activator.CreateInstance(typNullable, wynik);
                            Push(wynikNullable);
                        }
                        else
                        {
                            Push(wynik);
                        }
                    } else
                    {
                        //zwykły typ, więc go zwracam - takim jakim jest
                        Push(wynik);
                    }
                }

                
                WykonajNastepnaInstrukcje();
            }
            else if (md != null && md.IsSetter)
            {
                //wykonujemy settera
                var dane = Pop();
                instancja = Pop();
                var typ = instancja.GetType();
                
                var m2 = typ.GetProperty(md.Name.Replace("set_", ""));

                m2.SetValue(instancja, dane );
                WykonajNastepnaInstrukcje();
            }
            else
            {
                //wykonujemy metodę 
                int iloscArgumentow2 = mr.Parameters.Count;
                var arg = new List<object>();
                for (int i = 0; i < iloscArgumentow2; i++)
                {
                    var o = Pop();
                    arg.Add(o);
                }
                if (md.HasThis)
                {
                    //metoda ma this (metoda obiektu)
                    instancja = Pop();
                    arg.Reverse();
                    var argumenty = arg.ToArray();
                    var zmienioneArgumenty = new List<object>();
                    var obj = instancja;

                    //pobieram dostępne motody i sprawdzam najlepsze dopasowanie
                    var typDeklarujacy = md.DeclaringType.Resolve().GetSystemType();
                    if(typDeklarujacy == null)
                    {
                        typDeklarujacy = obj.GetType();
                    }
                    var meth = typDeklarujacy.GetMethods().Where(m=>m.Name == md.Name);

                    if (meth.Count() >= 1)
                    {
                        //mamy więcej metod o danej nazwie - sprawdzamy dopasowanie po parametrach
                        
                        foreach (var methodInfo in meth)
                        {
                            zmienioneArgumenty.Clear();

                            var parametry = methodInfo.GetParameters();
                            int i = 0;
                            bool czyZgodneParametry = true;
                            if(parametry.Count() != argumenty.Count())
                            {
                                //metoda o dobrej nazwie ale zle ilosci parametrow
                                continue;
                            }

                            foreach (var parameterInfo in parametry)
                            {
                                var argument = argumenty[i];
                                if (argument is int)
                                {
                                    //mamy albo int albo enum
                                    if (parameterInfo.ParameterType != typeof (int))
                                    {
                                        if (parameterInfo.ParameterType == typeof(bool))
                                        {
                                            var j = (int)argument;
                                            zmienioneArgumenty.Add(j == 1);
                                        }
                                        else if(!parameterInfo.ParameterType.IsEnum)
                                        {
                                            czyZgodneParametry = false;
                                            break;
                                        } else 
                                        {
                                            //zgodny ale enum zmieniam go na enum
                                            var e = Enum.ToObject(parameterInfo.ParameterType, (int)argument);
                                            zmienioneArgumenty.Add(e);
                                        }
                                    }
                                    else
                                    {
                                        //zgodny i int
                                        zmienioneArgumenty.Add(argument);
                                    }
                                }
                                else
                                {
                                    
                                    if (argument != null)
                                    {
                                        var argTyp = argumenty[i].GetType();
                                        czyTypySaTakieSame(argTyp, parameterInfo.ParameterType);
                                        if (czyTypySaTakieSame(argTyp, parameterInfo.ParameterType) == false)
                                        {
                                            czyZgodneParametry = false;
                                            break;
                                        }
                                        else
                                        {
                                            //zgodny null
                                            zmienioneArgumenty.Add(argument);
                                        }
                                    }
                                    else
                                    {
                                        if (parameterInfo.ParameterType.IsValueType == true)
                                        {
                                            czyZgodneParametry = false;
                                            break;
                                        }
                                        else
                                        {
                                            zmienioneArgumenty.Add(argument);
                                        }
                                    }
                                }
                                i++;
                            }
                            //sprawdzanie dopasowania dla tego motody zakończone
                            if (czyZgodneParametry == true)
                            {

                                //mamy dopasowaną metodę
                                //zamieniam listę argumentów
                                argumenty = zmienioneArgumenty.ToArray();
                                try
                                {
                                    if (gm != null && gm.HasGenericArguments == true)
                                    {
                                        var typ = instancja.GetType();
                                        var typParametruGenerycznego = gm.GenericArguments[0].GetSystemType();
                                        MethodInfo method = typ.GetMethod(mr.Name);
                                        MethodInfo generic = method.MakeGenericMethod(typParametruGenerycznego);
                                        var wynik = generic.Invoke(instancja, argumenty);
                                        Push(wynik);
                                    } else if (mr.ReturnType.FullName != typeof(void).FullName)
                                    {
                                        var wynik = methodInfo.Invoke(instancja, argumenty);
                                        Push(wynik);                                        
                                    } else
                                    {
                                        methodInfo.Invoke(instancja, argumenty);
                                    }

                                    WykonajNastepnaInstrukcje();
                                    return;
                                }
                                catch(Exception ex)
                                {

                                }
                                break;
                            }
                            
                        }
                    }
                    //jeśli tu dochodzimy to znaczy że nie udało się dopasować parametrów - lub jest jedna metoda
                    //uruchamiamy to przez interefejs dynamic

                    if (gm != null && gm.HasGenericArguments == true)
                    {
                        var typ = instancja.GetType();
                        var typParametruGenerycznego = gm.GenericArguments[0].GetSystemType();
                        MethodInfo method = typ.GetMethod(mr.Name);
                        MethodInfo generic = method.MakeGenericMethod(typParametruGenerycznego);
                        var wynik = generic.Invoke(instancja, argumenty);
                        Push(wynik);
                    }
                    else
                    {
                        if (mr.ReturnType.FullName != typeof(void).FullName && mr.HasGenericParameters == false)
                        {
                            var wynikDyn = Dynamic.InvokeMember(instancja, mr.Name, argumenty);
                            var wynik = (object)wynikDyn;
                            Push(wynik);
                        }
                        else if (mr.ReturnType.FullName != typeof(void).FullName && mr.HasGenericParameters == true)
                        {
                            var typ = instancja.GetType();
                            var genParam = md.GenericParameters[0];
                            var genD = genParam.Resolve();

                            var typParametruGenerycznego = md.GenericParameters[0].GetSystemType();
                            MethodInfo method = typ.GetMethod(mr.Name);
                            MethodInfo generic = method.MakeGenericMethod(typParametruGenerycznego);
                            var wynik = generic.Invoke(instancja, argumenty);
                            Push(wynik);
                        }
                        else
                        {
                            Dynamic.InvokeMemberAction(instancja, mr.Name, argumenty);
                        }
                    }
                    WykonajNastepnaInstrukcje();
                } else if( mr.Name.Equals("op_Equality"))
                {
                    dynamic a = arg[0];
                    dynamic b = arg[1];

                    dynamic wynik = a == b;
                    Push(wynik);
                    WykonajNastepnaInstrukcje();
                } else
                {
                    //metoda statyczna (bez this)
                    arg.Reverse();
                    var staticContext = InvokeContext.CreateStatic;
                    var typ = md.DeclaringType.GetSystemType();
                    var wynikDyn = Dynamic.InvokeMember(staticContext(typ), mr.Name, arg.ToArray());

                    var wynik = (object) wynikDyn;
                    if (mr.ReturnType.FullName != typeof(void).FullName)
                    {
                        Push(wynik);
                    }
                    WykonajNastepnaInstrukcje();
                }
                

                //wykonujemy metodę
                
            }
        }

        private bool czyTypySaTakieSame(Type t1, Type t2)
        {
            if (t1.IsGenericType != t2.IsGenericType)
            {
                return false;
            }

            if (t1.IsGenericType == false)
            {
                var t = t1;

                while(true)
                {
                    if(t == t2)
                    {
                        return true;
                    }

                    if(t == typeof(object))
                    {
                        return false;
                    }

                    t = t.BaseType;
                }
                return t1 == t2;
            }
            else
            {
                return t1.Name == t2.Name;
            }


            return true;
        }


        /// <summary>
        /// Metoda określa czy dane metoda ma być wykonana czy interpretowana.
        /// Interpretowana metoda jest rozbijana na assemblera i każda instrukcja jest interpretowana w wirtualnej maszynie
        /// "Wykonywane" motody są wykonywane na procesorze (tak jak wszystkie inne funkcje)
        /// </summary>
        /// <param name="md"></param>
        /// <returns>true - znaczy wykonywać
        ///         false - znaczy interpretować</returns>
        public bool CzyWykonacCzyInterpretowac(MethodReference mr)
        {
            var md = mr.Resolve();                      

            var czyKlasaMaAtrybut = md.DeclaringType.CustomAttributes.Any(a => a.AttributeType.FullName == typeof(InterpretAttribute).FullName);
            var czyMetodaMaAtrybut = md.CustomAttributes.Any(a => a.AttributeType.FullName == typeof(InterpretAttribute).FullName);

            if(md.IsSetter || md.IsGetter)
            {
                return true; //getery i setery zawssze wykonujemy
            }

            if(czyKlasaMaAtrybut || czyMetodaMaAtrybut)
            {
                return false; //interpertujemy
            }

            return true; //w innym wypadku wykonujemy metody
        }

        public override string ToString()
        {
            var md = instrukcja.Operand as MethodReference;
            return "Call " + md.Name.ToString();
        }
    }
}
