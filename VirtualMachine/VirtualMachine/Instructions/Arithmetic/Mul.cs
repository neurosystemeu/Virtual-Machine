using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Arithmetic
{
    public class Mul : InstructionBase
    {
        public Mul(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            dynamic b = PopObject();
            dynamic a = PopObject();

            dynamic wynik = a * b;
            PushObject(wynik);
            WykonajNastepnaInstrukcje();
        }
    }
}
