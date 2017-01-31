namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe
{
    public class Pracownik : ObiektBiznesowy
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public override bool Equals(object obj)
        {
            var prac = obj as Pracownik;
            if (prac != null)
            {
                return (prac.Imie?.Equals(Imie) ?? (Imie == null))
                    && (prac.Nazwisko?.Equals(Nazwisko) ?? (Nazwisko == null))
                    && base.Equals(prac);
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
