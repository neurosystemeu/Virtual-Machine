using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Arithmetic
{
    public class ArithmeticProcess : Proces
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double XX { get; set; }

        public override object Start()
        {
            int i = 2;
            XX = 1;
            double sum = 0;

            for (int j = 0; j < 10; j++)
            {
                X = j*Y + j/i;
                XX = 2.0*j + XX/2.0;
                XX = XX/(XX + 2.0);
                sum += XX;
            }

            return sum;
        }
    }
}
