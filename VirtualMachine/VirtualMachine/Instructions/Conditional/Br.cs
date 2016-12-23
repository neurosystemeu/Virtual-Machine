using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
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
            var op = instrukcja.Operand as Mono.Cecil.Cil.Instruction;
            var nextOffset = op.Offset;
            WykonajSkok(nextOffset);
        }
    }
}