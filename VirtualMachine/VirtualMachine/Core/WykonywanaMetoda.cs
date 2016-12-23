using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Klasy;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine
{
    

    public class WykonywanaMetoda
    {
        public WykonywanaMetoda()
        {
            LokalneArgumenty = new DaneMetody();
            LokalneZmienne = new DaneMetody();
            instrukcje = null;
        }

        public WykonywanaMetoda(MethodDefinition metoda) : this()
        {
            var m = this;
            m.AssemblyName = metoda.Module.FullyQualifiedName;
            m.NazwaTypu = metoda.DeclaringType.FullName;
            m.NazwaMetody = metoda.Name;
            m.NumerWykonywanejInstrukcji = 0;
        }

        public int NumerWykonywanejInstrukcji { get; set; }
        public DaneMetody LokalneArgumenty { get; set; }
        public DaneMetody LokalneZmienne { get; set; }

        private List<InstrukcjaBazowa> instrukcje;
        internal List<InstrukcjaBazowa> Instrukcje
        {
            get
            {
                if(instrukcje == null)
                {
                    instrukcje = PobierzInstrukcje();
                }
                return instrukcje;
            }
            set { instrukcje = value; }
        }

        public string NazwaTypu { get; set; }        
        public string NazwaMetody { get; set; }
        public string AssemblyName { get; internal set; }
        //public string AssemblyLocation { get; internal set; }
        public int OffsetWykonywanejInstrukcji { get; internal set; }

        public void WyczyscInstrukcje()
        {
            instrukcje = null;
        }               
        

        public List<InstrukcjaBazowa> PobierzInstrukcje()
        {
            var metoda = PobierzOpisMetody();
            var il = metoda.Body.GetILProcessor();
            var instrukcje = il.Body.Instructions.Select(i => InstrukcjaBazowa.UtworzInstrukcje(i));
            return instrukcje.ToList();
        }

        public int PobierzNumerInstrukcjiZOffsetem(int offset)
        {
            var inst = Instrukcje.FirstOrDefault(i => i.Offset == offset);
            return Instrukcje.IndexOf(inst);
        }

        public MethodDefinition PobierzOpisMetody()
        {
            var module = ModuleDefinition.ReadModule(this.AssemblyName);
            var typDef = module.Types.First(t => t.FullName == NazwaTypu);
            var metoda = typDef.Methods.FirstOrDefault(mm => mm.Name == NazwaMetody);
            return metoda;
        }

        public List<Mono.Cecil.Cil.ExceptionHandler> PobierzBlokiObslugiWyjatkow()
        {
            var lista = new List<Mono.Cecil.Cil.ExceptionHandler>();
            var metoda = PobierzOpisMetody();
            int offset = OffsetWykonywanejInstrukcji;
            
            foreach (var item in metoda.Body.ExceptionHandlers)
            {
                if(item.TryStart.Offset < offset && item.TryEnd.Offset > offset)
                {
                    lista.Add(item);
                }
            }

            return lista;
        }

        

        public bool CzyObslugujeWyjatki()
        {
            var metoda = PobierzOpisMetody();
            return metoda.Body.ExceptionHandlers.Count > 0;

        }

        public override string ToString()
        {
            return "Metoda " + NazwaMetody;
        }
    }
}
