using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    //Converts the boxed representation of a type specified in the instruction to its unboxed form. 
    internal class Unbox_Any : InstructionBase
    {
        public Unbox_Any(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {            
            WykonajNastepnaInstrukcje();
        }
    }
}