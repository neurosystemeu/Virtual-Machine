using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    //Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
    public class Dup : InstructionBase
    {
        public Dup(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var o = PopObject();
            PushObject(o);
            PushObject(o);

            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Dup";
        }
    }
}