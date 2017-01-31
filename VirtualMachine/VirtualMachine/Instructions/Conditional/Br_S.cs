using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    //Unconditionally transfers control to a target instruction (short form).
    public class Br_S : InstructionBase
    {
        public Br_S(Instruction ins):base(ins)
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
