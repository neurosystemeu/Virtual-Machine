using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Klasy;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instrukcje
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
                var typOperanda = ((Mono.Cecil.TypeReference)instrukcja.Operand).GetSystemType();
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