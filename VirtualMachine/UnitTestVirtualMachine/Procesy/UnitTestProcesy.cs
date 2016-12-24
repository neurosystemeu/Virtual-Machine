using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuroSystem.UnitTestVirtualMachine.Procesy.Podstawowy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy
{
    [TestClass]
    public class UnitTestProcesy
    {
        [TestMethod]
        public void Test_DodawaniePracownikaProces()
        {
            var proces = new DodawaniePracownikaProces();
            var vm = new VirtualMachine.VirtualMachine();
            var vmWynik = vm.Start(proces);

            var wynik = proces.Start();
            Assert.AreEqual(wynik, vmWynik);
        }

        [TestMethod]
        public void Test_RepozytoriumTestProces()
        {
            var proces = new RepozytoriumTestProces();
            var vm = new VirtualMachine.VirtualMachine();
            var vmWynik = vm.Start(proces);

            var wynik = proces.Start();
            Assert.AreEqual(wynik, vmWynik);
        }
    }
}
