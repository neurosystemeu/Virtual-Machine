using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Procesy
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDwochKonstruktorow()
        {
            var proces = new DwaParametryProces();
            var vm = new VirtualMachine.VirtualMachine();
            var vmWynik = vm.Start(proces);

            var wynik = proces.Start();
            Assert.AreEqual(wynik, vmWynik);
        }
    }
}
