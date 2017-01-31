using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Podstawowy
{
    public class DodawaniePracownikaProces : Proces
    {
        public override object Start()
        {
            var prac = new Pracownik();
            prac.Nazwisko = "Kowalski";
            prac.Imie = "Jan";

            return prac;
        }
    }
}
