using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Pop : InstructionBase
    {
        public Pop(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            PopObject();
            WykonajNastepnaInstrukcje();
        }
    }
}
