using System;
using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    ///Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
    public class Stelem_Ref : InstructionBase
    {
        public Stelem_Ref(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var val = PopObject();
            var indeks = (int) PopObject();
            var array = PopObject() as Array;

            array.SetValue(val,indeks);

           
            WykonajNastepnaInstrukcje();
        }
    }
}
