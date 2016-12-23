using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Klasy
{
    /// <summary>
    /// Dane metody
    /// </summary>
    public class MethodData
    {
        public MethodData()
        {
            Obiekty = new Dictionary<int, object>();
            //Obiekty = new object[4];
        }

        public void Ustaw(int index, object obiekt)
        {
            Obiekty[index] = obiekt;
        }

        public object Pobierz(int index)
        {
            if (Obiekty.ContainsKey(index) == false)
            {
                Obiekty[index] = null;                    
            }
            return Obiekty[index];
        }

        public Dictionary<int, object> Obiekty { get; set; }
        //public object[] Obiekty { get; set; }

        public override string ToString()
        {
            var str = Obiekty.Count + ": ";
            foreach (var item in Obiekty.Keys)
            {
                string wartosc = "";
                if (Obiekty.ContainsKey(item) != false)
                {
                    wartosc = Obiekty[item].ToString();
                }

                str += item.ToString() + "=" + wartosc + ";\n";
            }
            return str;
        }

        public void Wczytaj(object[] lista)
        {
            Obiekty.Clear();
            int i = 0;
            foreach (var item in lista)
            {
                Obiekty[i] = item;
                i++;
            }
        }
    }
}
