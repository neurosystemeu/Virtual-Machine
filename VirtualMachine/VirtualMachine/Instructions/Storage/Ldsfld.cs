using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Pushes the value of a static field onto the evaluation stack.
    /// https://msdn.microsoft.com/pl-pl/library/system.reflection.emit.opcodes.ldsfld(v=vs.110).aspx
    /// </summary>
    public class Ldsfld : InstructionBase
    {
        public Ldsfld(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            //pobieram statyczną zmienną
            var fieldDefinition = instrukcja.Operand as Mono.Cecil.FieldDefinition;
            var typ = fieldDefinition.DeclaringType.GetSystemType();
            var field = typ.GetField(fieldDefinition.Name);
            var val = field.GetValue(null);

            PushObject(val);
            WykonajNastepnaInstrukcje();
        }
    }
}
