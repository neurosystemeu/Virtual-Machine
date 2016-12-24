using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class CallStart : InstructionBase
    {
        public CallStart(Metoda metoda): base(null)
        {
            Metoda = metoda;            
        }

        public Metoda Metoda { get; set; }

        public override void Wykonaj()
        {
            WczytajLokalneArgumenty(1);
            var instancja = PobierzLokalnyArgument(0);

            Metoda.WyczyscInstrukcje();
            
            WirtualnaMaszyna.AktualnaMetoda = Metoda;            
            WirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji = 0;            
        }        
    }
}
