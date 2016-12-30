using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Leave_S : InstructionBase
    {
        public Leave_S(Instruction ins):base(ins)
        {
        }

        public override void Wykonaj()
        {
            var i = instrukcja.Operand as Mono.Cecil.Cil.Instruction;
            var nextOffset = i.Offset;
            WykonajSkok(nextOffset);
        }
    }
}
