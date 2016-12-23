using System;
using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Finds the value of a field in the object whose reference is currently on the evaluation stack.
    /// </summary>
    internal class Ldfld : InstructionBase
    {
        public Ldfld(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var fd = ((Mono.Cecil.FieldReference)instrukcja.Operand);
            var obiekt = Pop();
            var typ = obiekt.GetType();
            var prop = typ.GetProperty(fd.Name);
            if (prop != null)
            {
                var wartosc = prop.GetValue(obiekt);
                Push(wartosc);
            } else
            {
                var memb = typ.GetField(fd.Name);
                var wartosc = memb.GetValue(obiekt);
                Push(wartosc);
            }
            
            WykonajNastepnaInstrukcje();
        }
    }
}
