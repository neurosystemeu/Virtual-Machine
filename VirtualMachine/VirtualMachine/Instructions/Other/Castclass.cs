using NeuroSystem.VirtualMachine.Instrukcje;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Castclass : InstructionBase
    {
        public Castclass(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            WykonajNastepnaInstrukcje();
        }
    }
}