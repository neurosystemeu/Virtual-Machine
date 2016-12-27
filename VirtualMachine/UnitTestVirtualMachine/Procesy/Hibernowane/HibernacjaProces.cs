using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;
using NeuroSystem.VirtualMachine.Core.Attributes;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Hibernowane
{
    public class HibernacjaProces : Proces
    {
        public string Raport { get; set; }

        public override object Start()
        {
            PrzygotujRaport();

            var rep = new RepozytoriumTypowane<Pracownik>();
            var pracownicy = rep.PobierzObiektyTypowane();

            UzupelnijDanePracownikowRekurencyjnieIHibernuj(pracownicy, pracownicy.Count-1); //hibernuje się wewnątrz

            var pracownik = pracownicy.First();
            if (pracownik != null)
            {
                GenerujRaportPracownika(pracownik); //ta funkcja hibernuje proces
            }

            foreach (var pracownik1 in pracownicy)
            {
                GenerujRaportPracownika(pracownik1);
                VirtualMachine.VirtualMachine.Hibernate();
            }

            return Raport;
        }

        [Interpret]
        public void UzupelnijDanePracownikowRekurencyjnieIHibernuj(List<Pracownik> pracownicy, int index)
        {
            if (index < 0)
            {
                return;
            }

            var pracownik = pracownicy[index];
            pracownik.Nazwisko = "Kowalski " + index;
            VirtualMachine.VirtualMachine.Hibernate();
            pracownik.Imie = "Jan " + index;

            UzupelnijDanePracownikowRekurencyjnieIHibernuj(pracownicy, index - 1);
        }

        [Interpret]
        public void PrzygotujRaport()
        {
            Raport = "Raport: ";
        }

        [Interpret]
        public void GenerujRaportPracownika(Pracownik pracownik)
        {
            Raport += pracownik.Nazwisko;
            VirtualMachine.VirtualMachine.Hibernate(); //hibernuje porces w tym punkcie

            Raport += "Po zahibernowaniu " + pracownik.Imie + ";";
        }
    }
}
