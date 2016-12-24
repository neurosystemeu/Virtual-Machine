using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje.Klasy
{
    public class ArgumentAddress : ObjectAddressWraper
    {
        public int Indeks { get; internal set; }
        public MethodData LokalneArgumenty { get; internal set; }

        public override object GetValue()
        {
            if (LokalneArgumenty.Obiekty.ContainsKey(Indeks) == false)
            {
                LokalneArgumenty.Obiekty[Indeks] = null;
            }
            return LokalneArgumenty.Obiekty[Indeks];
        }

        public override void SetNull()
        {
            LokalneArgumenty.Obiekty[Indeks] = null;
        }

        public override string ToString()
        {
            return "Adres arg " + Indeks;
        }
    }
}
