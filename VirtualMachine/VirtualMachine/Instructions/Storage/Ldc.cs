using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Pushes a supplied value of type int32 onto the evaluation stack as an int32.
    /// https://msdn.microsoft.com/pl-pl/library/system.reflection.emit.opcodes.ldc_i4(v=vs.110).aspx
    /// </summary>
    public class Ldc : InstructionBase
    {
        public Ldc(object wartosc, Instruction instrukcja) : base(instrukcja)
        {
            obiekt = wartosc;
        }

        public object obiekt { get; set; }

        public override void Wykonaj()
        {
            PushObject(obiekt);
            WykonajNastepnaInstrukcje();
        }
    }
}
