using System;
using Mono.Reflection;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Conditional
{
    public class Isinst : InstructionBase
    {
        public Isinst(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            
            dynamic b = PopObject();
            if(b != null)
            {
                var typ = b.GetType();
                var typOperanda = (Type)instrukcja.Operand;
                if (typOperanda.IsAssignableFrom(typ))
                {
                    //mamy ten sam typ
                    PushObject(b);
                } else
                {
                    PushObject(null);
                }
            } else
            {
                PushObject(null);
            }
            
            WykonajNastepnaInstrukcje();
        }
    }
}