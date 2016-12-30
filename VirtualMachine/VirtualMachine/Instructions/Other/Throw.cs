using System;
using System.Linq;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Throw : InstructionBase
    {
        public Throw(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            WirtualnaMaszyna.Status = VirtualMachineState.Exception;
            var rzuconyWyjatek = PopObject();
            ObslugaRzuconegoWyjatku(WirtualnaMaszyna, rzuconyWyjatek);
        }

        public static void ObslugaRzuconegoWyjatku(VirtualMachine wirtualnaMaszyna, object rzuconyWyjatek)
        {
            var aktywnaMetod = wirtualnaMaszyna.AktualnaMetoda;
            while (true)
            {
                var czyObslugujeWyjatek = aktywnaMetod.CzyObslugujeWyjatki();
                if (czyObslugujeWyjatek)
                {
                    var bloki = aktywnaMetod.PobierzBlokiObslugiWyjatkow();
                    var blok = bloki.FirstOrDefault();
                    if (blok != null)
                    {
                        //obsługujemy pierwszys blok
                        wirtualnaMaszyna.AktualnaMetoda = aktywnaMetod;
                        wirtualnaMaszyna.AktualnaMetoda.NumerWykonywanejInstrukcji
                            = aktywnaMetod.PobierzNumerInstrukcjiZOffsetem(blok.HandlerStart.Offset);

                        wirtualnaMaszyna.Stos.PushObject(rzuconyWyjatek); 
                        if (blok.HandlerType == ExceptionHandlerType.Catch)
                        {
                            //wracam do zwykłej obsługi kodu                            
                            wirtualnaMaszyna.Status = VirtualMachineState.Executing;
                        }
                        
                        return;
                    }
                }

                aktywnaMetod = wirtualnaMaszyna.Stos.PobierzNastepnaMetodeZeStosu();
                if (aktywnaMetod == null)
                {
                    //mamy koniec stosu
                    throw new Exception("Koniec stosu w obsłudze wyjątku", rzuconyWyjatek as Exception);
                }
            }
        }
    }
}
