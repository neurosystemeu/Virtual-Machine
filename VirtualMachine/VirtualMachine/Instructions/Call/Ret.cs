using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Call
{
    public class Ret : InstructionBase
    {
        public Ret(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            //sprawdzam czy jest coś jeszcze na stosie
            if(WirtualnaMaszyna.Stos.IsEmpty())
            {
                //mamy koniec wykonywania procedury (bez wyniku) 
                WirtualnaMaszyna.CzyWykonywacInstrukcje = false;
                WirtualnaMaszyna.Status = VirtualMachineState.Executed;
                return;
            }

            //mamy wynik
            var dane = PopObject();
            if (dane is Metoda)
            {

                //mamy metodę która nie zwraca 
                var metodaDoWznowienia = dane as Metoda;
                WirtualnaMaszyna.AktualnaMetoda = metodaDoWznowienia;
                WirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji++;

            }
            else
            {
                //najpierw mamy wynik potem dane metody
                var wynik = dane;
                //sprawdzam czy jest coś jeszcze na stosie
                if (WirtualnaMaszyna.Stos.IsEmpty())
                {
                    //mamy koniec wykonywania funkcji (zwracającej wynik)
                    WirtualnaMaszyna.CzyWykonywacInstrukcje = false;
                    WirtualnaMaszyna.Status = VirtualMachineState.Executed;
                    WirtualnaMaszyna.Wynik = wynik;
                    return;
                }


                var metodaDoWznowienia = PopObject() as Metoda;
                PushObject(wynik); //zwracam wynik na stosie
                WirtualnaMaszyna.AktualnaMetoda = metodaDoWznowienia;
                WirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji++;
            }
        }
    }
}
