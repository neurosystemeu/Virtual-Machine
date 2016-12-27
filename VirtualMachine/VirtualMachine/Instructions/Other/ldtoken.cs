using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Klasy;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
    /// </summary>
    public class Ldtoken : InstructionBase
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
                PushObject(h);
                WykonajNastepnaInstrukcje();
            }
            var md = instrukcja.Operand as Mono.Cecil.MethodDefinition;
            if (md != null)
            {
                var typ = md.DeclaringType.GetSystemType();
                var typZwracany = md.ReturnType.GetSystemType();
                var metoda = typ.GetMethod(md.Name);
                var h = metoda.MethodHandle;
                
                PushObject(h);
                WykonajNastepnaInstrukcje();
            }
        }

        public override string ToString()
        {
            return "ldtoken ";
        }
    }
}