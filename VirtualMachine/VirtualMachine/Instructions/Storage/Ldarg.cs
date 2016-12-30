using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instructions.Storage
{
    public class Ldarg : InstructionBase
    {
        public Ldarg(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PobierzLokalnyArgument(Indeks);
            PushObject(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Indeks;
        }
    }
}
