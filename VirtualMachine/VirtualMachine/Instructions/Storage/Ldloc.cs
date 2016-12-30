using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Loads the local variable at a specific index onto the evaluation stack.
    /// </summary>
    public class Ldloc : InstructionBase
    {
        public Ldloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PobierzLokalnaZmienna(Indeks);
            PushObject(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Indeks;
        }
    }
}
