using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Klasy;
using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    //Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
    public class Newarr : InstrukcjaBazowa
    {
        public Newarr(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var tr = instrukcja.Operand as Mono.Cecil.TypeReference;
            var td = tr.Resolve();

            var typ = td.GetSystemType();
            var n = (int)Pop();
            object arr = Array.CreateInstance(typ, n);
            Push(arr);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Newarr ";
        }
    }
}