using NeuroSystem.VirtualMachine.Instrukcje;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    //Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
    class Ceq : InstrukcjaBazowa
    {
        public Ceq(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            dynamic b = Pop();
            dynamic a = Pop();
            if (a is int && b is bool)
            {
                b = b ? 1 : 0;
            }
            else if (b is int && a is bool)
            {
                a = a ? 1 : 0;
            }

            dynamic wynik = a == b;
            Push(wynik);
            WykonajNastepnaInstrukcje();
        }
    }
}