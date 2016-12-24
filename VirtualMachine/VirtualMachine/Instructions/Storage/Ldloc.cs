using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Loads the local variable at a specific index onto the evaluation stack.
    /// </summary>
    public class Ldloc : InstructionBase
    {
        public Ldloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PobierzLokalnaZmienna(Indeks);
            PushObject(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Ldloc " + Indeks;
        }
    }
}
