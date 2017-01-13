using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var retFromSerializedVM = vm.Resume();
            
            var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
            var retInProcProces = inProcProces.Start();
            Assert.AreEqual(retInProcProces, retFromSerializedVM);
        }
    }
}
