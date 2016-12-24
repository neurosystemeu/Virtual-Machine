using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Add : InstructionBase
    {
        public Add(Instruction instrukcja) : base(instrukcja)
        {            
        }

        public override void Wykonaj()
        {
            dynamic b = PopObject();
            dynamic a = PopObject();
            
            dynamic wynik = a + b;
            PushObject(wynik);
            WykonajNastepnaInstrukcje();
        }
    }
}
