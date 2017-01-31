using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Nop : InstructionBase
    {
        public Nop(Instruction instrukcja) : base(instrukcja)
        {
        }
        public override void Wykonaj()
        {
            WykonajNastepnaInstrukcje();
        }
    }
}
