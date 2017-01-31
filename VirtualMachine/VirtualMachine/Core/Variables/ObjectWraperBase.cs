using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Core.Variables
{
    public class ObjectWraperBase : ElementBase
    {
        public virtual object GetValue()
        {
            return null;
        }

        public virtual void SetNull()
        {
            
        }
    }
}
