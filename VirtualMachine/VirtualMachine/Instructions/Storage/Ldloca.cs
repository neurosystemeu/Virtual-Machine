using System;
using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    //Loads the address of the local variable at a specific index onto the evaluation stack
    public class Ldloca : InstructionBase
    {
        public Ldloca(object operand, Instruction instrukcja) : base(instrukcja)
        {
            Operand = operand;
        }

        public object Operand { get; set; }

        public override void Wykonaj()
        {
            var vr = Operand as System.Reflection.LocalVariableInfo;
            
            var o = PobierzAdresZmiennejLokalnej(vr.LocalIndex);
            Push(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Operand;
        }
    }
}
