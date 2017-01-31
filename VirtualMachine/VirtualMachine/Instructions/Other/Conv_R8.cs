using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    /// <summary>
    /// Converts the value on top of the evaluation stack to float64.
    /// </summary>
    internal class Conv_R8 : InstructionBase
    {
        public Conv_R8(Instruction ins) : base(ins)
        {
        }       

        public override void Wykonaj()
        {
            dynamic a = PopObject();

            dynamic wynik = (double)a;
            PushObject(wynik);            

            WykonajNastepnaInstrukcje();
        }
    }
}