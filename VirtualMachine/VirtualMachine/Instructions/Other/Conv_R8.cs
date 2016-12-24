using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
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