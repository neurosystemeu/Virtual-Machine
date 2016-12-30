using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
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
            var obiekt = PopObject();
            var typ = obiekt.GetType();
            var prop = typ.GetProperty(fd.Name);
            if (prop != null)
            {
                var wartosc = prop.GetValue(obiekt);
                PushObject(wartosc);
            } else
            {
                var memb = typ.GetField(fd.Name);
                var wartosc = memb.GetValue(obiekt);
                PushObject(wartosc);
            }
            
            WykonajNastepnaInstrukcje();
        }
    }
}
