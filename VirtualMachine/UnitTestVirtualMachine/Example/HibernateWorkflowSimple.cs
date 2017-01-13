using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SomeFunctionNotInterpreted();
            }

            //hibernate executed method
            VirtualMachine.VirtualMachine.Hibernate();

            //after restore (in another thread/computer)
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeFunctionNotInterpreted();
            }

            return "Helow World " + InputParametr;
        }

        public void SomeFunctionNotInterpreted()
        {
            //do some work
            InputParametr++;
        }


    }
}
