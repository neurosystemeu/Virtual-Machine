using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuroSystem.UnitTestVirtualMachine.Procesy.Podstawowy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Fluent
{
    [TestClass]
    public class FluentUITest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var proces = new FluentUIProcess();
            var vm = new VirtualMachine.VirtualMachine();
            var vmWynik = vm.Start(proces);
            proces = vm.Instance as FluentUIProcess;

            proces = new FluentUIProcess();
            var wynik = proces.Start();
            Assert.AreEqual(wynik, vmWynik);
        }
    }
}
