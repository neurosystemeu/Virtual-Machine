using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Constrained : InstructionBase
    {
        public Constrained(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            
            WykonajNastepnaInstrukcje();
        }
    }
}