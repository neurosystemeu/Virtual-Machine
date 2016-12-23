using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Instrukcje;
using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    ///Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
    public class Stelem_Ref : InstructionBase
    {
        public Stelem_Ref(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var val = Pop();
            var indeks = (int) Pop();
            var array = Pop() as Array;

            array.SetValue(val,indeks);

           
            WykonajNastepnaInstrukcje();
        }
    }
}
