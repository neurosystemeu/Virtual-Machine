using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Klasy;
using Polenter.Serialization;

namespace NeuroSystem.VirtualMachine
{
    public class VirtualMachine
    {
        public VirtualMachine()
        {
            Stos = new Stos();
            WirtualnaMaszynaDebug = this; // do debugowania        
        }

        public TD Start<T, TD>(T instancja, Expression<Func<T, TD>> startMethod, bool czyWykonywac = true)
        {
            WalidujMetodyObiektu(instancja);

            var member = (startMethod.Body as MethodCallExpression).Method;
            var nazwa = member.Name;

            Start(instancja, nazwa, czyWykonywac);

            if (Wynik == null)
            {
                return default(TD);
            }

            return (TD)Wynik;
        }

        public void Start(object instancja, string nazwaMetodyStartu, bool czyWykonywac = true)
        {
            NumerIteracji = 0;
            Instance = instancja;
            var typ = Instance.GetType();
            var foldre = typ.Assembly.Location;
            var module = Mono.Cecil.ModuleDefinition.ReadModule(foldre);
            var typy = module.GetTypes();
            var typDef = typy.First(t => t.FullName == typ.FullName);
            var metoda = typDef.Methods.FirstOrDefault(mm => mm.Name == nazwaMetodyStartu);


            var m = new WykonywanaMetoda(metoda);

            AktualnaMetoda = m;
            CzyWykonywacInstrukcje = true;

            Stos.Push(instancja);
            m.Instrukcje = new List<InstrukcjaBazowa>() { new CallStart(m) };

            if (czyWykonywac)
            {
                Wykonuj();
            }
            else
            {
                //pierwsza instrukcja to CallStart(m) - która nie serializuje się, musi być teraz wykonana - dla ustawienia 
                //odpowiedniego stosu i stanu wirutalnej maszyny
                aktualnaInstrukcja = PobierzAktualnaInstrukcje();
                aktualnaInstrukcja.Wykonaj();
                NumerIteracji++;
            }
        }

        public static VirtualMachine WirtualnaMaszynaDebug;

        public void Wykonuj()
        {
            WirtualnaMaszynaDebug = this; // do debugowania 

            while (CzyWykonywacInstrukcje)
            {
                try
                {
                    aktualnaInstrukcja = PobierzAktualnaInstrukcje();
                    aktualnaInstrukcja.Wykonaj();
                    NumerIteracji++;
                }
                catch (Exception ex)
                {
                    CzyWykonywacInstrukcje = false;
                    Status = EnumStatusWirtualnejMaszyny.RzuconyWyjatek;
                    //RzuconyWyjatekCalosc = ex.ToString();
                    RzuconyWyjatekWiadomosc = ex.Message; ;
                    if (Debugger.IsAttached)
                    {
                        throw;
                    }
                    return;
                }
            }
        }

        public void WykonujKrokowo(long iloscKrokow)
        {
            var WirtualnaMaszyna = this; // do debugowania 

            while (CzyWykonywacInstrukcje && iloscKrokow > 0)
            {
                aktualnaInstrukcja = PobierzAktualnaInstrukcje();
                aktualnaInstrukcja.Wykonaj();
                NumerIteracji++;
                iloscKrokow--;
            }
        }

        public void Resume()
        {
            Status = EnumStatusWirtualnejMaszyny.Wykonana;
            CzyWykonywacInstrukcje = true;
            AktualnaMetoda.NumerWykonywanejInstrukcji++;

            Wykonuj();
        }

        public void HibernujWirtualnaMaszyne()
        {
            CzyWykonywacInstrukcje = false;
            Status = EnumStatusWirtualnejMaszyny.Zahibernowana;
        }


        /// <summary>
        /// Metoda służy do hibernowania wirtualnej maszyny
        /// Wywoływana z procesu który interpretowany jest przez wirtualną maszynę
        /// </summary>
        public static void Hibernate()
        {

        }

        public InstrukcjaBazowa PobierzAktualnaInstrukcje()
        {
            var ai = AktualnaMetoda.Instrukcje[AktualnaMetoda.NumerWykonywanejInstrukcji];
            ai.WirtualnaMaszyna = this;
            return ai;
        }

        public void WalidujMetodyObiektu(object instancjaObiektu)
        {
            var typ = instancjaObiektu.GetType();
            var foldre = typ.Assembly.Location;
            var module = Mono.Cecil.ModuleDefinition.ReadModule(foldre);
            var typy = module.GetTypes();

            var typDef = typy.First(t => t.FullName == typ.FullName);
            var metody = typDef.Methods;
            foreach (var metoda in metody)
            {
                var m = new WykonywanaMetoda(metoda);
                var i = m.PobierzInstrukcje(); //pobierma instrukcje metody - jeśli brakuje jakiejś instrukcji rzuca wyjątek
            }
        }



        public object Instance { get; set; }
        public Stos Stos { get; set; }
        public WykonywanaMetoda AktualnaMetoda { get; set; }
        public EnumStatusWirtualnejMaszyny Status { get; set; }
        public string RzuconyWyjatekCalosc { get; set; }
        public string RzuconyWyjatekWiadomosc { get; set; }
        public bool CzyWykonywacInstrukcje { get; set; }
        public object Wynik { get; internal set; }
        public long NumerIteracji { get; set; }
        private InstrukcjaBazowa aktualnaInstrukcja;

        internal string DebugInfo
        {
            get
            {
                var s = NumerIteracji.ToString() + ": " +
                    AktualnaMetoda.NazwaMetody + " ";
                if (aktualnaInstrukcja != null)
                {
                    s += String.Format("0x{0:X}", aktualnaInstrukcja.Offset);
                    if (aktualnaInstrukcja.instrukcja != null)
                    {
                        s += ": " + aktualnaInstrukcja;
                    }
                }
                return s;
            }
        }

        public string Serialize()
        {
            var xml = SerializeObject(this);
            return xml;
        }

        public static VirtualMachine Deserialize(string xml)
        {
            var obiekt = DeserializeObject(xml);
            return (VirtualMachine)obiekt;
        }


        private static string SerializeObject(object obj)
        {
            if (obj == null)
            {
                return "<null/>";
            }

            var serializer = new SharpSerializer();
            //serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(obj, ms);
                ms.Position = 0;
                byte[] bajty = ms.ToArray();
                return Encoding.UTF8.GetString(bajty, 0, bajty.Length);
            }
        }

        private static object DeserializeObject(string xmlOfAnObject)
        {
            if (xmlOfAnObject.StartsWith("?"))
            {
                xmlOfAnObject = xmlOfAnObject.Remove(0, 1);
            }

            if (xmlOfAnObject.Equals("<null/>"))
            {
                return null;
            }

            xmlOfAnObject = xmlOfAnObject.Replace("System.Linq.Expressions.TypedParameterExpression, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                , "NeuroSystem.VirtualMachine.Serializacja.NsTypedParameterExpression, NeuroSystem.VirtualMachine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");


            var serializer = new SharpSerializer();
            serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            //serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));
            byte[] bajty = Encoding.UTF8.GetBytes(xmlOfAnObject);
            using (var ms = new MemoryStream(bajty))
            {
                object obiekt = serializer.Deserialize(ms);

                return obiekt;
            }
        }
    }
}
