﻿using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    //Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
    public class Clt : InstructionBase
    {
        public Clt(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            dynamic b = PopObject();
            dynamic a = PopObject();

            dynamic wynik = a < b ? 1 : 0;
            PushObject(wynik);
            WykonajNastepnaInstrukcje();
        }
    }
}