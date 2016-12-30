using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    public class Newobj : InstructionBase
    {
        public Newobj(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var md = instrukcja.Operand as MethodReference;
            var typMono = md.DeclaringType;
            var typ = typMono.GetSystemType();
            var iloscParametrow = md.Parameters.Count;
            var listaParametrow = new List<Object>();
            for (int i = 0; i < iloscParametrow; i++)
            {
                listaParametrow.Add(PopObject());
            }

            //md.Module.ExportedTypes.FirstOrDefault(t=>t.)
            var nowyObiekt = Activator.CreateInstance(typ,listaParametrow.ToArray());
            PushObject(nowyObiekt);
            WykonajNastepnaInstrukcje();
        }
    }
}
