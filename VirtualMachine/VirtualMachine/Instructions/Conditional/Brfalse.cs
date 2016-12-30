using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    //Transfers control to a target instruction if value is false, a null reference, or zero.
    public class Brfalse : InstructionBase
    {
        public Brfalse(Instruction instrukcja) : base(instrukcja)
        {            
        }
        
        public override void Wykonaj()
        {
            var wynik = false;
            dynamic a = PopObject();
            if (a == null)
            {
                wynik = false;
            }
            else if (a is bool)
            {
                wynik = (bool)a;
            }
            else if(a is int)
            {
                wynik = a == 1;
            } else
            {
                wynik = true;
            }

            var op = instrukcja.Operand as Mono.Cecil.Cil.Instruction;
            var nextOffset = op.Offset;
            if (wynik == false)
            {
                WykonajSkok(nextOffset);
            }
            else
            {
                WykonajNastepnaInstrukcje();
            }
        }
    }
}