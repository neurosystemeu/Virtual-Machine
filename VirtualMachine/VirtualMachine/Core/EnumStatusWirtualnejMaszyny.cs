using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Klasy
{
    public enum VirtualMachineState
    {
        Stoped,
        Executing,
        Exception,
        Executed,
        Hibernated
    }
}
