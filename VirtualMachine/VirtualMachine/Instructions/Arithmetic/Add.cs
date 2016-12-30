using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Arithmetic
{
    public class Add : InstructionBase
    {
        public Add(Instruction instrukcja) : base(instrukcja)
        {            
        }

        public override void Wykonaj()
        {
            dynamic b = PopObject();
            dynamic a = PopObject();
            
            dynamic wynik = a + b;
            PushObject(wynik);
            WykonajNastepnaInstrukcje();
        }
    }
}
