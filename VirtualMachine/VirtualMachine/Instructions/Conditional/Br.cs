using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    /// <summary>
    /// Unconditionally transfers control to a target instruction.
    /// </summary>
    internal class Br : InstructionBase
    {
        public Br(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {
            var op = instrukcja.Operand as Instruction;
            var nextOffset = op.Offset;
            WykonajSkok(nextOffset);
        }
    }
}