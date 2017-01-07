using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    /// <summary>
    /// Compares two unsigned or unordered values. 
    /// If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; 
    /// otherwise 0 (int32) is pushed onto the evaluation stack.
    /// </summary>
    internal class Cgt_Un : InstructionBase
    {
        public Cgt_Un(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {
            dynamic b = PopObject();
            dynamic a = PopObject();

            if(b is int || a is int)
            {
                dynamic wynik = a > b ? 1 : 0;
                PushObject(wynik);
            } else if (b is double || a is double)
            {
                dynamic wynik = a > b ? 1 : 0;
                PushObject(wynik);
            }
            else if (b is float || a is float)
            {
                dynamic wynik = a > b ? 1 : 0;
                PushObject(wynik);
            } else
            {
                //mamy jakiś obiekt więc sprawdzamy czy jest różny
                dynamic wynik = a != b ? 1 : 0;
                PushObject(wynik);
            }


            WykonajNastepnaInstrukcje();
        }
    }
}