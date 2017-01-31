using System;
using System.Collections.Generic;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;

namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria
{
    /// <summary>
    /// Testowe repozytorium
    /// </summary>
    public class Repozytorium
    {
        public virtual List<ObiektBiznesowy> PobierzObiekty()
        {
            return null;
        }

        public virtual List<ObiektBiznesowy> PobierzObiekty(string filtry)
        {
            return null;
        }

        public virtual List<ObiektBiznesowy> PobierzObiekty(string filtry, DateTime od)
        {
            return null;
        }

        public virtual List<ObiektBiznesowy> PobierzObiekty(string filtry, DateTime? od)
        {
            return null;
        }

        public virtual List<ObiektBiznesowy> PobierzObiekty(string filtry, DateTime? od, DateTime? _do)
        {
            return null;
        }
    }
}
