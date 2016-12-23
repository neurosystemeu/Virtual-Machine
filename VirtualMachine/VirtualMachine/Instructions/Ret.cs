using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using Mono.Cecil;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Ret : InstrukcjaBazowa
    {
        public Ret(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            //sprawdzam czy jest coś jeszcze na stosie
            if(WirtualnaMaszyna.Stos.CzyPusty())
            {
                //mamy koniec wykonywania procedury (bez wyniku) 
                WirtualnaMaszyna.CzyWykonywacInstrukcje = false;
                WirtualnaMaszyna.Status = EnumStatusWirtualnejMaszyny.Wykonana;
                return;
            }

            //mamy wynik
            var dane = Pop();
            if (dane is WykonywanaMetoda)
            {

                //mamy metodę która nie zwraca 
                var metodaDoWznowienia = dane as WykonywanaMetoda;
                WirtualnaMaszyna.AktualnaMetoda = metodaDoWznowienia;
                WirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji++;

            }
            else
            {
                //najpierw mamy wynik potem dane metody
                var wynik = dane;
                //sprawdzam czy jest coś jeszcze na stosie
                if (WirtualnaMaszyna.Stos.CzyPusty())
                {
                    //mamy koniec wykonywania funkcji (zwracającej wynik)
                    WirtualnaMaszyna.CzyWykonywacInstrukcje = false;
                    WirtualnaMaszyna.Status = EnumStatusWirtualnejMaszyny.Wykonana;
                    WirtualnaMaszyna.Wynik = wynik;
                    return;
                }


                var metodaDoWznowienia = Pop() as WykonywanaMetoda;
                Push(wynik); //zwracam wynik na stosie
                WirtualnaMaszyna.AktualnaMetoda = metodaDoWznowienia;
                WirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji++;
            }
        }
    }
}
