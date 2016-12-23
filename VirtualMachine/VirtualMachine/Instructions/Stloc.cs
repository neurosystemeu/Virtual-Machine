using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Stloc : InstrukcjaBazowa
    {
        public Stloc(int indeks, Instruction instrukcja) : base(instrukcja)
        {
            Indeks = indeks;
        }

        public int Indeks { get; set; }

        public override void Wykonaj()
        {
            var o = Pop();
            ZapiszLokalnaZmienna(o, Indeks);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Stloc " + Indeks;
        }
    }
}
