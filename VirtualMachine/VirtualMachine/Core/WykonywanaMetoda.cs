using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using NeuroSystem.VirtualMachine.Core.Components;
using NeuroSystem.VirtualMachine.Instructions;

namespace NeuroSystem.VirtualMachine.Core
{
    
    /// <summary>
    /// Metoda która będzie wykonywana
    /// </summary>
    public class Metoda : ElementBase
    {
        public Metoda()
        {
            LocalArguments = new MethodData();
            LocalVariables = new MethodData();
            instrukcje = null;
        }

        public Metoda(MethodDefinition metoda) : this()
        {
            var m = this;
            m.AssemblyName = metoda.Module.FullyQualifiedName;
            m.NazwaTypu = metoda.DeclaringType.FullName;
            m.NazwaMetody = metoda.Name;
            m.NumerWykonywanejInstrukcji = 0;
        }

        #region Propercje

        public string NazwaTypu { get; set; }
        public string NazwaMetody { get; set; }
        public string AssemblyName { get; internal set; }
        public int OffsetWykonywanejInstrukcji { get; internal set; }


        public int NumerWykonywanejInstrukcji { get; set; }
        public MethodData LocalArguments { get; set; }
        public MethodData LocalVariables { get; set; }

        private List<InstructionBase> instrukcje;
        internal List<InstructionBase> Instrukcje
        {
            get
            {
                if (instrukcje == null)
                {
                    instrukcje = PobierzInstrukcjeMetody();
                }
                return instrukcje;
            }
            set { instrukcje = value; }
        }

        #endregion



        #region Instrukcje

        public void WyczyscInstrukcje()
        {
            instrukcje = null;
        }

        public List<InstructionBase> PobierzInstrukcjeMetody()
        {
            var metoda = PobierzOpisMetody();
            var il = metoda.Body.GetILProcessor();
            var instrukcje = il.Body.Instructions.Select(i => InstructionBase.UtworzInstrukcje(i));
            return instrukcje.ToList();
        }

        #endregion









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
            return NazwaMetody + " 0x" + OffsetWykonywanejInstrukcji.ToString("X") +  " " + NazwaTypu + " " + AssemblyName;
        }
    }
}
