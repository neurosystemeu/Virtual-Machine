﻿using System;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine.Core;

namespace NeuroSystem.VirtualMachine.Instructions.Other
{
    //Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
    public class Newarr : InstructionBase
    {
        public Newarr(Instruction instrukcja) : base(instrukcja)
        {
        }

        public override void Wykonaj()
        {
            var tr = instrukcja.Operand as Mono.Cecil.TypeReference;
            var td = tr.Resolve();

            var typ = td.GetSystemType();
            var n = (int)PopObject();
            object arr = Array.CreateInstance(typ, n);
            PushObject(arr);
            WykonajNastepnaInstrukcje();
        }

        public override string ToString()
        {
            return "Newarr ";
        }
    }
}