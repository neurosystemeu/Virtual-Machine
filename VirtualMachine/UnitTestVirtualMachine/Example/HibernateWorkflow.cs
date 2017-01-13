using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Core.Attributes;

namespace NeuroSystem.UnitTestVirtualMachine.Example
{
    public class HibernateWorkflow
    {
        public int InputParametr { get; set; }
        public string Start()
        {
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            //after restore (in another thread/computer)
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            return "Helow World " + InputParametr;
        }

        [Interpret]
        public void SomeInterpretedFunction()
        {
            //do some work
            InputParametr++;

            //hibernate executed method
            VirtualMachine.VirtualMachine.Hibernate();
        }


    }
}
