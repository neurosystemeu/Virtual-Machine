using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Arithmetic
{
    [TestClass]
    public class UnitTestArithmetic
    {
        [TestMethod]
        public void ArithmeticTest()
        {
            var proces = new ArithmeticProcess();
            var vm = new VirtualMachine.VirtualMachine();
            var vmWynik = vm.Start(proces);
            proces = vm.Instance as ArithmeticProcess;

            proces = new ArithmeticProcess();
            var wynik = proces.Start();
            Assert.AreEqual(wynik, vmWynik);
        }
    }
}
