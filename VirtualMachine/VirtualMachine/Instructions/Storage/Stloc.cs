using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.
    /// </summary>
    public class Stloc : InstructionBase
    {
        public Stloc(Instruction instrukcja) : base(instrukcja)
        {
            
        }

        public int Indeks
        {
            get
            {
                var str = instrukcja.OpCode.Name.Split('.')[1];
                return int.Parse(str);
            }
        }
       
        public override void Wykonaj()
        {
            var o = PopObject();

            var a = instrukcja.Operand as System.Reflection.LocalVariableInfo;
            if (a != null)
            {
                ZapiszLokalnaZmienna(o, a.LocalIndex);
            }
            else
            {
                ZapiszLokalnaZmienna(o, Indeks);
            }
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString();// + " " + Indeks;
        }
    }
}
