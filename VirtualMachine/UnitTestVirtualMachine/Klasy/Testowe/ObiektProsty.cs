using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Testowe
{
    public class ObiektProsty
    {
        public class WewnetrznyObiekt
        {
            public int Wartosc { get; set; }
        }

        public class WewnetrznyObiektGeneryczny<T>
        {
            public int Wartosc { get; set; }

            public T GetT()
            {
                return default(T);
            }
        }

        public string Nazwa { get; set; }

        public WewnetrznyObiekt PobierzWewnetrznyObiekt()
        {
            return new WewnetrznyObiekt();
        }

    }
}
