using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    public class Starg : InstructionBase
    {
        public Starg(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PopObject();
            ZapiszLokalnyArgument(o, Indeks);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Indeks;
        }
    }
}
