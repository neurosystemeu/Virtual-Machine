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
    public class Ldtoken : InstrukcjaBazowa
    {
        public Ldtoken(Instruction instrukcja) : base(instrukcja)
        {
        }
        
        public override void Wykonaj()
        {
            var td =  instrukcja.Operand as Mono.Cecil.TypeDefinition;
            if (td != null)
            {
                var typ = td.GetSystemType();
                var h = typ.TypeHandle;
                Push(h);
                WykonajNastepnaInstrukcje();
            }
            var md = instrukcja.Operand as Mono.Cecil.MethodDefinition;
            if (md != null)
            {
                var typ = md.DeclaringType.GetSystemType();
                var typZwracany = md.ReturnType.GetSystemType();
                var metoda = typ.GetMethod(md.Name);
                var h = metoda.MethodHandle;
                
                Push(h);
                WykonajNastepnaInstrukcje();
            }
        }

        public override string ToString()
        {
            return "ldtoken ";
        }
    }
}