using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Procesy
{
    public class DwaParametryProces : Proces
    {
        public DwaParametryProces()
        {

        }

        public DwaParametryProces(int i, DateTime data)
        {
            Data = data;
        }

        public DateTime Data { get; set; }

        public override object Start()
        {
            var proces = new DwaParametryProces(4, DateTime.Today);

            return proces.Data;
        }
    }
}
