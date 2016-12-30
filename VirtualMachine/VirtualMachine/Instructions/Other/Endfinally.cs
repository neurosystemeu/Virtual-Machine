using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Endfinally : InstructionBase
    {
        public Endfinally(Instruction ins):base(ins)
        {
        }

        public override void Wykonaj()
        {
            var rzuconyWyjatek = PopObject();
            if (WirtualnaMaszyna.Status == VirtualMachineState.Exception)
            {
                //jestem w trakcie wyjątku, przechodzę przez stos do obsługi wyjątku
                Throw.ObslugaRzuconegoWyjatku(WirtualnaMaszyna, rzuconyWyjatek);
            } else
            {
                WykonajNastepnaInstrukcje();
            }
        }
    }
}
