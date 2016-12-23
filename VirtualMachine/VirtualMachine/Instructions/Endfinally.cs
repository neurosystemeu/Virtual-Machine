using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Klasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Endfinally : InstrukcjaBazowa
    {
        public Endfinally(Instruction ins):base(ins)
        {
        }

        public override void Wykonaj()
        {
            var rzuconyWyjatek = Pop();
            if (WirtualnaMaszyna.Status == EnumStatusWirtualnejMaszyny.RzuconyWyjatek)
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
