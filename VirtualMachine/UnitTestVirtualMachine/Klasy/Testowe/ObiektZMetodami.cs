using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.UnitTestVirtualMachine.Klasy.Testowe
{
    /// <summary>
    /// Klasa do testowania wykonywania dynamicznie metod
    /// </summary>
    public class ObiektZMetodami
    {
        
        public int Metoda()
        {
            return 0;
        }

        public int Metoda(DateTime? data)
        {
            return 1;
        }

        public int Metoda(DateTime data)
        {
            return 2;
        }

        public int Metoda(DateTime? data, DateTime data2)
        {
            return 3;
        }

        public int Metoda<T>(T t1)
        {
            return 4;
        }

        public int Metoda<T>(T t1, DateTime? data)
        {
            return 5;
        }

        public int Metoda<T>(T t1, DateTime data)
        {
            return 6;
        }

        public int Metoda<T, T2>(T t1, T2 t2, DateTime data)
        {
            return 7;
        }

        public static int MetodaStatyczna(DateTime? date)
        {
            return 8;
        }

        public static int MetodaStatyczna(DateTime date)
        {
            return 9;
        }

        public static int MetodaStatyczna<T>(DateTime date, T t1)
        {
            return 10;
        }

        public static List<T> MetodaStatyczna<T>(T t1)
        {
            return null; //11
        }
    }
}
