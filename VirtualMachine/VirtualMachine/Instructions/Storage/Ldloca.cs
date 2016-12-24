using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    //Loads the address of the local variable at a specific index onto the evaluation stack
    public class Ldloca : InstructionBase
    {
        public Ldloca(object operand, Instruction instrukcja) : base(instrukcja)
        {
            Operand = operand;
        }

        public object Operand { get; set; }

        public override void Wykonaj()
        {
            var vr = Operand as VariableReference;
            var vd = vr.Resolve();

            var o = PobierzAdresZmiennejLokalnej(vr.Index);
            PushObject(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Ldloca " + Operand;
        }
    }
}
