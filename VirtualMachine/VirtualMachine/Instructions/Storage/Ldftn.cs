using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Reflection;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.
    /// https://msdn.microsoft.com/pl-pl/library/system.reflection.emit.opcodes.ldftn(v=vs.110).aspx
    /// </summary>
    public class Ldftn : InstructionBase
    {
        public Ldftn(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var methodDefiniton = instrukcja.Operand;
            PushObject(methodDefiniton);
            WykonajNastepnaInstrukcje();
        }
    }
}
