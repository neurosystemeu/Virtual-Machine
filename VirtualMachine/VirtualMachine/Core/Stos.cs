using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine
{
    public class Stos
    {
        public Stos()
        {
            stosWewnetrzny = new Stack<object>();
        }

        public Stack<object> PobierzStos()
        {
            return stosWewnetrzny;
        }

        private Stack<object> stosWewnetrzny;

        /// <summary>
        /// Używany tylko do serializacji stosu (zapisuje go jako lista)
        /// </summary>
        public List<object> StosSerializowany
        {
            get
            {
                return stosWewnetrzny.ToList();
            }
            set
            {
                var l = value.ToList();
                l.Reverse();
                foreach (var item in l)
                {
                    stosWewnetrzny.Push(item);
                }
            }
        }

        public void Push(object obiekt)
        {
            stosWewnetrzny.Push(obiekt);
        }

        public object Pop()
        {
            return stosWewnetrzny.Pop();
        }

        public bool CzyPusty()
        {
            return stosWewnetrzny.Count == 0;
        }

        public override string ToString()
        {
            var str = stosWewnetrzny.Count + " :";

            foreach (var item in stosWewnetrzny.ToList())
            {
                if (item != null)
                {
                    str += item.ToString() + ";\n";
                }
                else
                {
                    str += "null;\n";
                }
            }

            return str;
        }

        public WykonywanaMetoda PobierzNastepnaMetodeZeStosu()
        {
            while(stosWewnetrzny.Count > 0)
            {
                var o = stosWewnetrzny.Pop();
                if(o is WykonywanaMetoda)
                {
                    return o as WykonywanaMetoda;
                }
            }

            return null;
        }

        public object PobierzElementZeStosu(int numerElementuOdSzczytu)
        {
            var tablicaElementowStosu = stosWewnetrzny.ToArray();
            return tablicaElementowStosu[numerElementuOdSzczytu];
        }
    }
}
