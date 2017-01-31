using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Hibernowane
{
    public class ProstaHibernacjaProces : Proces
    {
        public int State { get; set; }
        public override object Start()
        {
            State = 1;
            VirtualMachine.VirtualMachine.Hibernate(); //informuję VM że chce zatrzymać wykonanie i zahibernować vm
            //które wyonuje daną funkcję

            State = 2;
            VirtualMachine.VirtualMachine.Hibernate(); //ponownie hibernuje maszynę

            State = 3;

            return State;
        }
    }
}
