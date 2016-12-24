using System;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Instrukcje.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
    /// </summary>
    public class Initobj : InstructionBase
    {
        public Initobj(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {
            var ob = Pop();

            if(ob == null)
            {
                //nic nie robimy
            } else if( ob is ObjectAddressWraper)
            {
                //mamy adres do zmiennej
                var adres = ob as ObjectAddressWraper;
                var zmienna = adres.GetValue();

                if(zmienna != null)
                {
                    //TODO: coś trzeba zrobić , pewnie zerować wszystkie propercje
                    var insturkcjaGeneryczna = this.instrukcja.Operand as Mono.Cecil.GenericInstanceType;
                    if (insturkcjaGeneryczna != null && insturkcjaGeneryczna.FullName.StartsWith("System.Nullable"))
                    {
                        adres.SetNull();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            //TODO: Coś trzeba tu jeszcze chyba zrobić

            WykonajNastepnaInstrukcje();
        }
    }
}