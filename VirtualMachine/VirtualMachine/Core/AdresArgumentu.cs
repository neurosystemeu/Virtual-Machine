using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje.Klasy
{
    public class AdresArgumentu : AdresZmiennej
    {
        public int Indeks { get; internal set; }
        public DaneMetody LokalneArgumenty { get; internal set; }

        public override object PobierzZmienna()
        {
            if (LokalneArgumenty.Obiekty.ContainsKey(Indeks) == false)
            {
                LokalneArgumenty.Obiekty[Indeks] = null;
            }
            return LokalneArgumenty.Obiekty[Indeks];
        }

        public override void UstawNull()
        {
            LokalneArgumenty.Obiekty[Indeks] = null;
        }

        public override string ToString()
        {
            return "Adres arg " + Indeks;
        }
    }
}
