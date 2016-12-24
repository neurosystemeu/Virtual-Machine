using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje.Klasy
{
    public class LocalVariableAddress : ObjectAddressWraper
    {
        public int Indeks { get; internal set; }
        public MethodData LokalneZmienne { get; internal set; }

        public override object GetValue()
        {
            if(LokalneZmienne.Obiekty.ContainsKey(Indeks)==false)
            {
                LokalneZmienne.Obiekty[Indeks] = null;
            }
            return LokalneZmienne.Obiekty[Indeks];
        }

        public override void SetNull()
        {
            LokalneZmienne.Obiekty[Indeks] = null;
        }

        public override string ToString()
        {
            return "Adres zmiennej lok. " + Indeks;
        }
    }
}
