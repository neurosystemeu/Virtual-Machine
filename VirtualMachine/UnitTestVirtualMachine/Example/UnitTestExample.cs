using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.UnitTestVirtualMachine.Example
{
    [TestClass]
    public class UnitTestExample
    {
        [TestMethod]
        public void TestMethod1()
        {
            var proces = new HibernateWorkflowSimple() { InputParametr = 10 };
            var vm = new VirtualMachine.VirtualMachine();
            vm.Start(proces);
            var serializedVMXml = vm.Serialize();

            
            var vmNew = VirtualMachine.VirtualMachine.Deserialize(serializedVMXml);
            var retFromSerializedVM = vmNew.Resume();
            
            var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
            var retInProcProces = inProcProces.Start();
            Assert.AreEqual(retInProcProces, retFromSerializedVM);
        }

        [TestMethod]
        public void TestMethodMultiHibernation()
        {
            var proces = new HibernateWorkflowSimple() { InputParametr = 10 };
            var vm = new VirtualMachine.VirtualMachine();
            vm.Start(proces);
            var serializedVMXml = vm.Serialize();
            object retFromSerializedVM = "";

            while (vm.Status == VirtualMachineState.Hibernated)
            {
                vm = VirtualMachine.VirtualMachine.Deserialize(serializedVMXml);
                retFromSerializedVM = vm.Resume();
                serializedVMXml = vm.Serialize();
            }

            var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
            var retInProcProces = inProcProces.Start();
            Assert.AreEqual(retInProcProces, retFromSerializedVM);
        }
    }
}
