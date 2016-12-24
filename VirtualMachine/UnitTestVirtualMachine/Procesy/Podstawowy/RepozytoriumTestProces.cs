using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Podstawowy
{
    public class RepozytoriumTestProces : Proces
    {
        public override object Start()
        {
            var rep = new RepozytoriumTypowane<Pracownik>();
            var pracownicy = rep.PobierzObiektyTypowane();

            int i = 0;
            foreach (var pracownik in pracownicy)
            {
                pracownik.Imie = "Prac " + i;
                i++;
            }
            return pracownicy;
        }
    }
}
