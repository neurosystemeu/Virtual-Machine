using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Constrained : InstrukcjaBazowa
    {
        public Constrained(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            
            WykonajNastepnaInstrukcje();
        }
    }
}