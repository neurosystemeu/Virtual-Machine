using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Testowe;
using NeuroSystem.VirtualMachine.Core;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.UnitTestVirtualMachine.MonoCecilToReflection
{
    /// <summary>
    /// Sprawdzanie konwersji typów z Mono.Cecil.TypeReference -> System.Type
    /// </summary>
    [TestClass]
    public class MonoCecilToSystemTypeTest
    {
        public static ModuleDefinition GetModule()
        {
            var folder = Assembly.GetExecutingAssembly().Location;
            var module = Mono.Cecil.ModuleDefinition.ReadModule(folder);
            return module;
        }


        [TestMethod]
        public void TestMethod1()
        {
            var typ = typeof(Pracownik);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();
            
            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ);
            var systemObiekt = Activator.CreateInstance(systemType);
            Assert.AreEqual(obiekt, systemObiekt);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var typ = typeof(List<Pracownik>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as List<Pracownik>;
            var systemObiekt = Activator.CreateInstance(systemType) as List<Pracownik>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
            Assert.IsTrue(obiekt.Count == systemObiekt.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var typ = typeof(List<List<Pracownik>>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as List<List<Pracownik>>;
            var systemObiekt = Activator.CreateInstance(systemType) as List<List<Pracownik>>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
            Assert.IsTrue(obiekt.Count == systemObiekt.Count);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var typ = typeof(List<RepozytoriumTypowane<Pracownik>>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as List<RepozytoriumTypowane<Pracownik>>;
            var systemObiekt = Activator.CreateInstance(systemType) as List<RepozytoriumTypowane<Pracownik>>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
            Assert.IsTrue(obiekt.Count == systemObiekt.Count);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var typ = typeof(Dictionary<int, Pracownik>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as Dictionary<int, Pracownik>;
            var systemObiekt = Activator.CreateInstance(systemType) as Dictionary<int, Pracownik>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
            Assert.IsTrue(obiekt.Count == systemObiekt.Count);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var typ = typeof(ObiektGeneryczny2<Dictionary<int, Pracownik>, ModuleDefinition>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as ObiektGeneryczny2<Dictionary<int, Pracownik>, ModuleDefinition>;
            var systemObiekt = Activator.CreateInstance(systemType) as ObiektGeneryczny2<Dictionary<int, Pracownik>, ModuleDefinition>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
        }


        [TestMethod]
        public void TestMethod7()
        {
            var typ = typeof(ObiektProsty.WewnetrznyObiekt);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as ObiektProsty.WewnetrznyObiekt;
            var systemObiekt = Activator.CreateInstance(systemType) as ObiektProsty.WewnetrznyObiekt;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var typ = typeof(ObiektProsty.WewnetrznyObiektGeneryczny<int>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as ObiektProsty.WewnetrznyObiektGeneryczny<int>;
            var systemObiekt = Activator.CreateInstance(systemType) as ObiektProsty.WewnetrznyObiektGeneryczny<int>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var typ = typeof(ObiektProsty.WewnetrznyObiektGeneryczny<ObiektProsty.WewnetrznyObiekt>);

            var module = GetModule();
            var cecilType = module.Import(typ);
            var systemType = cecilType.GetSystemType();

            Assert.AreEqual(typ, systemType);

            var obiekt = Activator.CreateInstance(typ) as ObiektProsty.WewnetrznyObiektGeneryczny<ObiektProsty.WewnetrznyObiekt>;
            var systemObiekt = Activator.CreateInstance(systemType) as ObiektProsty.WewnetrznyObiektGeneryczny<ObiektProsty.WewnetrznyObiekt>;
            Assert.IsNotNull(obiekt);
            Assert.IsNotNull(systemObiekt);
        }

        
    }
}
