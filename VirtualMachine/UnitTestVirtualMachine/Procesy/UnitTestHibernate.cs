using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuroSystem.UnitTestVirtualMachine.Procesy.Hibernowane;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy
{
    [TestClass]
    public class UnitTestHibernate
    {
        [TestMethod]
        public void TestMethod1()
        {
            var proces = new ProstaHibernacjaProces();
            var vm = new VirtualMachine.VirtualMachine();
            vm.Start(proces); //zwraca null, bo proces się zahibernował
            Assert.IsTrue(vm.Status == VirtualMachineState.Hibernated);

            vm.Resume(); //wznawiam wykonanie procesu -> po czym proces powinien znów się zahibernować
            Assert.IsTrue(vm.Status == VirtualMachineState.Hibernated);

            vm.Resume(); //wznawiam wykonanie procesu -> po czym proces powinien się zakończyć i zwrócić wynik wewnętrzny
            Assert.IsTrue(vm.Status == VirtualMachineState.Executed);

            var vmWynik = vm.Wynik;

            var wynik = proces.Start();

            Assert.AreEqual(wynik, vmWynik);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var proces = new HibernacjaProces();
            var wynik = proces.Start(); //wynik procesu wykonanego 'natywnie'

            proces = new HibernacjaProces();

            var vm = new VirtualMachine.VirtualMachine();
            vm.Start(proces); //zwraca null, bo proces się zahibernował

            //wznawiam tyle razy ile potrzeba, tak żeby zakończyć proces
            while (vm.Status == VirtualMachineState.Hibernated)
            {
                vm.Resume();
            }

            var vmWynik = vm.Wynik; //wynik procesu interpretowanego w VM


            Assert.AreEqual(wynik, vmWynik);
        }
    }
}
