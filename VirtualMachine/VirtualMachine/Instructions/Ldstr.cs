using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Ldstr : InstrukcjaBazowa
    {
        public Ldstr(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            string str = instrukcja.Operand as string;
            Push(str);
            WykonajNastepnaInstrukcje();
        }
    }
}
