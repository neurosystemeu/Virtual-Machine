using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    /// <summary>
    /// Transfers control to a target instruction (short form) if two values are equal.
    /// https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.beq_s(v=vs.110).aspx
    /// </summary>
    public class Beq : InstructionBase
    {
        public Beq(Instruction ins) : base(ins)
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