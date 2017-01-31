using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Loads the local variable at a specific index onto the evaluation stack.
    /// </summary>
    public class Ldloc : InstructionBase
    {
        public Ldloc(Instruction instrukcja) : base(instrukcja)
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

            var a = instrukcja.Operand as System.Reflection.LocalVariableInfo;
            if (a != null)
            {
                var o = PobierzLokalnaZmienna(a.LocalIndex);
                PushObject(o);
            }
            else
            {
                var o = PobierzLokalnaZmienna(Indeks);
                PushObject(o);
            }

            
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + instrukcja.OpCode.Name;
        }
    }
}
