using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;

namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria
{
    /// <summary>
    /// Testowe repozytorium typowane
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepozytoriumTypowane<T> : Repozytorium
        where T : ObiektBiznesowy, new ()
    {
        public virtual List<T> PobierzObiektyTypowane()
        {
            var lista = new List<T>();
            lista.Add(new T());
            lista.Add(new T());
            return lista;
        }
    }
}
