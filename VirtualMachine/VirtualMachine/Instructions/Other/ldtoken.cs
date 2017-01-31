using System;
using System.Reflection;
using Mono.Reflection;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Other
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
            //throw new NotImplementedException("instrukcja Ldtoken");
            var td = instrukcja.Operand as Type;
            if (td != null)
            {
                var typ = td;
                var h = typ.TypeHandle;
                PushObject(h);
                WykonajNastepnaInstrukcje();
                return;
            }
            
            var md = instrukcja.Operand as MethodInfo;
            if (md != null)
            {
                //var typ = md.DeclaringType;
                //var typZwracany = md.ReturnType;
                //var metoda = typ.GetMethod(md.Name);
                var h = md.MethodHandle;
                
                PushObject(h);
                WykonajNastepnaInstrukcje();
                return;
            }

            throw new NotImplementedException("instrukcja Ldtoken");
        }

        public override string ToString()
        {
            return "ldtoken ";
        }
    }
}