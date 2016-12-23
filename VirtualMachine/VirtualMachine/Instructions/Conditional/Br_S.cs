using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    //Unconditionally transfers control to a target instruction (short form).
    public class Br_S : InstructionBase
    {
        public Br_S(Instruction ins):base(ins)
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
