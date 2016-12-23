using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje.Klasy
{
    public class AdresZmiennejLokalnej : AdresZmiennej
    {
        public int Indeks { get; internal set; }
        public DaneMetody LokalneZmienne { get; internal set; }

        public override object PobierzZmienna()
        {
            if(LokalneZmienne.Obiekty.ContainsKey(Indeks)==false)
            {
                LokalneZmienne.Obiekty[Indeks] = null;
            }
            return LokalneZmienne.Obiekty[Indeks];
        }

        public override void UstawNull()
        {
            LokalneZmienne.Obiekty[Indeks] = null;
        }

        public override string ToString()
        {
            return "Adres zmiennej lok. " + Indeks;
        }
    }
}
