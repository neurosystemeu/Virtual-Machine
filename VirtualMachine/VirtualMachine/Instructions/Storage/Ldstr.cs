using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    public class Ldstr : InstructionBase
    {
        public Ldstr(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            string str = instrukcja.Operand as string;
            PushObject(str);
            WykonajNastepnaInstrukcje();
        }
    }
}
