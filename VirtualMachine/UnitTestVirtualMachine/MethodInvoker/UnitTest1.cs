using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Cecil;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Repozytoria;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Testowe;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.UnitTestVirtualMachine.MethodInvoker
{
    [TestClass]
    public class UnitTest1
    {
        public static ModuleDefinition GetModule()
        {
            var folder = Assembly.GetExecutingAssembly().Location;
            var module = Mono.Cecil.ModuleDefinition.ReadModule(folder);
            return module;
        }

        /// <summary>
        /// Testuje wykonywanie odpowiednije metody na podstawie nazwy w stringu
        /// oraz obiektów (parametrów)
        /// 
        /// </summary>
        [TestMethod]
        public void TestUruchamianiaMetodyProstych()
        {
            var obiekt = new ObiektZMetodami();
            var typ = obiekt.GetType();

            var module = GetModule();
            var monoType = module.Types.First(t => t.Name == "ObiektZMetodami");

            // metoda 0
            var numerMetody = 0;
            var monoMethod = monoType.Methods[numerMetody];
            var pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            var wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt);
            Assert.AreEqual(wynik, numerMetody);

            // metoda 1
            numerMetody = 1;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {null});
            Assert.AreEqual(wynik, numerMetody);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, DateTime.Now);
            Assert.AreEqual(wynik, numerMetody);

            // metoda 2
            numerMetody = 2;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {null});
            Assert.AreEqual(wynik, numerMetody);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, DateTime.Now);
            Assert.AreEqual(wynik, numerMetody);

            // metoda 3
            numerMetody = 3;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {null, DateTime.Now});
            Assert.AreEqual(wynik, numerMetody);
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, DateTime.Now, DateTime.Now);
            Assert.AreEqual(wynik, numerMetody);

        }

        [TestMethod]
        public void TestUruchamianiaMetodyGenerycznych()
        {
            var obiekt = new ObiektZMetodami();
            var typ = obiekt.GetType();

            var module = GetModule();
            var monoType = module.Types.First(t => t.Name == "ObiektZMetodami");

            // metoda 4 - metoda generyczna - public int Metoda<T>(T t1) - T = DateTime?
            var numerMetody = 4;
            var monoMethod = monoType.Methods[numerMetody];
            var pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(DateTime?));
            var wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {null});
            Assert.AreEqual(wynik, numerMetody);

            // metoda 4 - metoda generyczna - public int Metoda<T>(T t1) - T = DateTime
            numerMetody = 4;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(DateTime));
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {DateTime.Now});
            Assert.AreEqual(wynik, numerMetody);

            // metoda 5 - metoda generyczna - public int Metoda<T>(T t1, DateTime? data) - T = DateTime
            numerMetody = 5;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(DateTime));
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {DateTime.Now, null});
            Assert.AreEqual(wynik, numerMetody);

            // metoda 6 - metoda generyczna - public int Metoda<T>(T t1, DateTime data) - T = DateTime
            numerMetody = 6;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(DateTime));
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {DateTime.Now, DateTime.Now});
            Assert.AreEqual(wynik, numerMetody);

            // metoda 7 - metoda generyczna - public int Metoda<T, T2>(T t1, T2 t2, DateTime data) - T = int, T2 = Decimal
            numerMetody = 7;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(int), typeof(Decimal));
            wynik = pobierzObiekty_filtr_do.Wykonaj(obiekt, new object[] {1, new decimal(2), DateTime.Now});
            Assert.AreEqual(wynik, numerMetody);
        }

        [TestMethod]
        public void TestUruchamianiaMetodyStatyczne()
        {
            var obiekt = new ObiektZMetodami();
            var typ = obiekt.GetType();

            var module = GetModule();
            var monoType = module.Types.First(t => t.Name == "ObiektZMetodami");

            // metoda 8 
            var numerMetody = 8;
            var monoMethod = monoType.Methods[numerMetody];
            var pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            var wynik = pobierzObiekty_filtr_do.Wykonaj(null, new object[] { null });
            Assert.AreEqual(wynik, numerMetody);

            // metoda 8 
            numerMetody = 8;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            wynik = pobierzObiekty_filtr_do.Wykonaj(null, new object[] { DateTime.Now });
            Assert.AreEqual(wynik, numerMetody);

            // metoda 9 
            numerMetody = 9;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            wynik = pobierzObiekty_filtr_do.Wykonaj(null, new object[] { DateTime.Now });
            Assert.AreEqual(wynik, numerMetody);

            // metoda 10 public static int MetodaStatyczna<T>(DateTime date, T t1)
            numerMetody = 10;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod( typeof(Decimal));
            wynik = pobierzObiekty_filtr_do.Wykonaj(null, new object[] { DateTime.Now, new decimal(5), });
            Assert.AreEqual(wynik, numerMetody);


            // metoda 11 public static List<T> MetodaStatyczna<T>(T t1)
            numerMetody = 11;
            monoMethod = monoType.Methods[numerMetody];
            pobierzObiekty_filtr_do = typ.GetMethod(monoMethod);
            pobierzObiekty_filtr_do = pobierzObiekty_filtr_do.MakeGenericMethod(typeof(Decimal));
            wynik = pobierzObiekty_filtr_do.Wykonaj(null, new object[] { new decimal(5) });
            Assert.IsNull(wynik);
        }
}
}
