using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Core.Attributes;

namespace NeuroSystem.UnitTestVirtualMachine.Example
{
    public class HibernateWorkflowSimple
    {
        public int InputParametr { get; set; }
        public string Start()
        {
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            //hibernate executed method
            VirtualMachine.VirtualMachine.Hibernate();

            //after resume
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            return "Helow World " + InputParametr;
        }

        public void SomeInterpretedFunction()
        {
            //do some work
            InputParametr++;
        }
    }
}
