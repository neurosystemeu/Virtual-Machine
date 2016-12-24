using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Ldc : InstructionBase
    {
        public Ldc(object wartosc, Instruction instrukcja) : base(instrukcja)
        {
            obiekt = wartosc;
        }

        public object obiekt { get; set; }

        public override void Wykonaj()
        {
            PushObject(obiekt);
            WykonajNastepnaInstrukcje();
        }
    }
}
