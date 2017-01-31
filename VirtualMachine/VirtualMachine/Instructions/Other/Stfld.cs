using System;
using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    /// <summary>
    /// Replaces the value stored in the field of an object reference or pointer with a new value.
    /// </summary>
    internal class Stfld : InstructionBase
    {
        public Stfld(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            throw new Exception("Brak implementacji instrukcji Stfld");

            WykonajNastepnaInstrukcje();
        }
    }
}
