using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Core.Variables.Value
{
    public class ObjectWraper : ObjectWraperBase
    {
        public object Warosc;

        public ObjectWraper(object o)
        {
            Warosc = o;
        }

        public override object GetValue()
        {
            return Warosc;
        }

        public override string ToString()
        {
            return "OW: " + Warosc;
        }
    }
}
