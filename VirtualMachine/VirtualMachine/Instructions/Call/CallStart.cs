using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Call
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
