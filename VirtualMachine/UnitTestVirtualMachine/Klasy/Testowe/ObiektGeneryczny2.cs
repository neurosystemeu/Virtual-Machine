using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Testowe
{
    public class ObiektGeneryczny2<T, T2> : ObiektBiznesowy
    {
        public T GetT()
        {
            return default(T);
        }

        public T2 GetT2()
        {
            return default(T2);
        }
    }
}
