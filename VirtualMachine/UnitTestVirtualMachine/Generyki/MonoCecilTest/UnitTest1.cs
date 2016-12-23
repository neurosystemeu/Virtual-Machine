using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil.Rocks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;

namespace NeuroSystem.UnitTestVirtualMachine.Generyki.MonoCecilTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var typ = this.GetType();
            var foldre = typ.Assembly.Location;
            var module = Mono.Cecil.ModuleDefinition.ReadModule(foldre);

            var mtRepozytorium = module.Types.First(p => p.Name == "Repozytorium");
            var mtRepozytoriumTypowane = module.Types.First(p => p.Name == "RepozytoriumTypowane`1");
            var mtPobierzOb = mtRepozytoriumTypowane.Methods.First(m => m.Name == "PobierzObiektyTypowane");

            var repozytorim = new RepozytoriumTypowane<Pracownik>();
            var tRepozytorim = repozytorim.GetType();
        }
    }
}
