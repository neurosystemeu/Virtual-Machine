using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Ldloc : InstrukcjaBazowa
    {
        public Ldloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = PobierzLokalnaZmienna(Indeks);
            Push(o);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Ldloc " + Indeks;
        }
    }
}
