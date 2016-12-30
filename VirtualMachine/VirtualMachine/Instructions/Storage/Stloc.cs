using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    /// <summary>
    /// Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.
    /// </summary>
    public class Stloc : InstructionBase
    {
        public Stloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PopObject();
            ZapiszLokalnaZmienna(o, Indeks);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Indeks;
        }
    }
}
