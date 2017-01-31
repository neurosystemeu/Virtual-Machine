using NeuroSystem.VirtualMachine.Core.Components;

namespace NeuroSystem.VirtualMachine.Core.Variables.Addresses
{
    public class LocalVariableAddress : ObjectAddressWraper
    {
        public int Indeks { get; set; }
        public MethodData LokalneZmienne { get; set; }

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
