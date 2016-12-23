using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Pop : InstrukcjaBazowa
    {
        public Pop(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            Pop();
            WykonajNastepnaInstrukcje();
        }
    }
}
