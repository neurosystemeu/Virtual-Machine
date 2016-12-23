using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.
    /// </summary>
    public class Stloc : InstructionBase
    {
        public Stloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = Pop();
            ZapiszLokalnaZmienna(o, Indeks);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Stloc " + Indeks;
        }
    }
}
