using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
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
