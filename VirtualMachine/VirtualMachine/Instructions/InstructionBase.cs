using System;
using System.Linq;
using Mono.Reflection;
using NeuroSystem.VirtualMachine.Core;
using NeuroSystem.VirtualMachine.Core.Variables;
using NeuroSystem.VirtualMachine.Core.Variables.Addresses;
using NeuroSystem.VirtualMachine.Instructions.Arithmetic;
using NeuroSystem.VirtualMachine.Instructions.Boxing;
using NeuroSystem.VirtualMachine.Instructions.Call;
using NeuroSystem.VirtualMachine.Instructions.Conditional;
using NeuroSystem.VirtualMachine.Instructions.Other;
using NeuroSystem.VirtualMachine.Instructions.Storage;
using System.Reflection.Emit;

namespace NeuroSystem.VirtualMachine.Instructions
{
    public class InstructionBase
    {
        public InstructionBase(Instruction ins)
        {
            instrukcja = ins;
            if (ins != null)
            {
                Offset = ins.Offset;
            }
        }

        public Instruction instrukcja;
        public int Offset { get; set; }
        public System.Reflection.Emit.OpCode KodInstrukcji { get; set; }
        public VirtualMachine WirtualnaMaszyna { get; set; }

        public virtual void Wykonaj()
        {
            throw new NotImplementedException();
        }

        public void WczytajLokalneArgumenty(int iloscArgumentow)
        {
            var lista = new object[iloscArgumentow];
            for (int i = iloscArgumentow - 1; i >= 0; i--)
            {
                var o = WirtualnaMaszyna.Stos.Pop();
                lista[i] = o;
            }
            WirtualnaMaszyna.AktualnaMetoda.LocalArguments.Wczytaj(lista);
        }

        public void ZapiszLokalnyArgument(object o, int indeks)
        {
            WirtualnaMaszyna.AktualnaMetoda.LocalArguments.Ustaw(indeks, o);
        }

        public object PobierzLokalnyArgument(int indeks)
        {
            var obiekt = WirtualnaMaszyna.AktualnaMetoda.LocalArguments.Pobierz(indeks);
            var ow = obiekt as ObjectWraperBase;
            if (ow != null)
            {
                return ow.GetValue();
            }
            return obiekt;
        }

        public void ZapiszLokalnaZmienna(object o, int indeks)
        {
            WirtualnaMaszyna.AktualnaMetoda.LocalVariables.Ustaw(indeks, o);
        }

        public object PobierzLokalnaZmienna(int indeks)
        {
            var obiekt = WirtualnaMaszyna.AktualnaMetoda.LocalVariables.Pobierz(indeks);
            var ow = obiekt as ObjectWraperBase;
            if (ow != null)
            {
                return ow.GetValue();
            }
            return obiekt;
        }

        public LocalVariableAddress PobierzAdresZmiennejLokalnej(int indeks)
        {
            var adres = new LocalVariableAddress();
            adres.Indeks = indeks;
            adres.LokalneZmienne = WirtualnaMaszyna.AktualnaMetoda.LocalVariables;

            return adres;
        }

        public ArgumentAddress PobierzAdresArgumentu(int indeks)
        {
            var adres = new ArgumentAddress();
            adres.Indeks = indeks;
            adres.LokalneArgumenty = WirtualnaMaszyna.AktualnaMetoda.LocalArguments;

            return adres;
        }

        public void WykonajNastepnaInstrukcje()
        {
            var am = WirtualnaMaszyna.AktualnaMetoda;
            am.NumerWykonywanejInstrukcji++;
            am.OffsetWykonywanejInstrukcji
                = am.Instrukcje[am.NumerWykonywanejInstrukcji].Offset;
        }

        public void WykonajSkok(int nowyOffset)
        {
            var am = WirtualnaMaszyna.AktualnaMetoda;
            var ins = am.Instrukcje.FirstOrDefault(i => i.Offset == nowyOffset);
            am.NumerWykonywanejInstrukcji = am.Instrukcje.IndexOf(ins);
        }

        public void PushObject(object o)
        {
            WirtualnaMaszyna.Stos.PushObject(o);
        }

        public void Push(ElementBase o)
        {
            WirtualnaMaszyna.Stos.Push(o);
        }

        /// <summary>
        /// Zwraca obiekt
        /// jeśli jest adres na stosie to zamienia na obiekt
        /// </summary>
        /// <returns></returns>
        public object PopObject()
        {
            var ob = WirtualnaMaszyna.Stos.Pop();
            if (ob is ObjectWraperBase)
            {
                var v = ob as ObjectWraperBase;
                return v.GetValue();
            }

            return ob;
        }

        public object Pop()
        {
            var ob = WirtualnaMaszyna.Stos.Pop();

            return ob;
        }

        public object PobierzElementZeStosu(int numerElementuOdSzczytu)
        {
            return WirtualnaMaszyna.Stos.PobierzElementZeStosu(numerElementuOdSzczytu);
        }

        public override string ToString()
        {
            return GetType().Name + " 0x" + WirtualnaMaszyna.AktualnaMetoda.OffsetWykonywanejInstrukcji.ToString("X");
        }

        public static InstructionBase UtworzInstrukcje(Instruction instrukcja)
        {
            try
            {


                switch (instrukcja.OpCode.Name)
                {
                    case "nop":
                        return new Nop(instrukcja);
                    case "ldarg.0":
                        return new Ldarg(0, instrukcja);
                    case "ldarg.1":
                        return new Ldarg(1, instrukcja);
                    case "ldarg.2":
                        return new Ldarg(2, instrukcja);
                    case "ldarg.3":
                        return new Ldarg(3, instrukcja);
                    case "ldarga.s":
                        return new Ldarga(instrukcja);
                    case "starg.s":
                        return new Starg(((Mono.Cecil.ParameterDefinition)instrukcja.Operand).Index, instrukcja);
                    case "call":
                        return new Call.Call(instrukcja);
                    case "callvirt":
                        return new Callvirt(instrukcja);
                    case "ret":
                        return new Ret(instrukcja);
                    case "add":
                        return new Add(instrukcja);
                    case "stloc.s":
                        return new Stloc(instrukcja);
                    case "stloc.0":
                        return new Stloc(instrukcja);
                    case "stloc.1":
                        return new Stloc(instrukcja);
                    case "stloc.2":
                        return new Stloc(instrukcja);
                    case "stloc.3":
                        return new Stloc(instrukcja);
                    case "ldloc.s":
                        return new Ldloc(instrukcja);
                    case "ldloc.0":
                        return new Ldloc( instrukcja);
                    case "ldloc.1":
                        return new Ldloc(instrukcja);
                    case "ldloc.2":
                        return new Ldloc(instrukcja);
                    case "ldloc.3":
                        return new Ldloc(instrukcja);
                    case "br.s":
                        return new Br_S(instrukcja);
                    case "sub":
                        return new Sub(instrukcja);
                    case "ldstr":
                        return new Ldstr(instrukcja);
                    case "newobj":
                        return new Newobj(instrukcja);
                    case "throw":
                        return new Throw(instrukcja);
                    case "leave.s":
                        return new Leave_S(instrukcja);
                    case "endfinally":
                        return new Endfinally(instrukcja);
                    case "pop":
                        return new Pop(instrukcja);
                    case "ldc.i4.m1":
                        return new Ldc(-1, instrukcja);
                    case "ldc.i4.0":
                        return new Ldc(0, instrukcja);
                    case "ldc.i4.1":
                        return new Ldc(1, instrukcja);
                    case "ldc.i4.2":
                        return new Ldc(2, instrukcja);
                    case "ldc.i4.3":
                        return new Ldc(3, instrukcja);
                    case "ldc.i4.4":
                        return new Ldc(4, instrukcja);
                    case "ldc.i4.5":
                        return new Ldc(5, instrukcja);
                    case "ldc.i4.6":
                        return new Ldc(6, instrukcja);
                    case "ldc.i4.s":
                        return new Ldc((sbyte)instrukcja.Operand, instrukcja);
                    case "ldc.i4":
                        return new Ldc((Int32)instrukcja.Operand, instrukcja);
                    case "ldc.r8":
                        return new Ldc(instrukcja.Operand, instrukcja);
                    case "ldnull":
                        return new Ldc(null, instrukcja);
                    case "ldloca.s":
                        return new Ldloca(instrukcja.Operand, instrukcja);
                    case "cgt":
                        return new Cgt(instrukcja);
                    case "clt":
                        return new Clt(instrukcja);
                    case "ceq":
                        return new Ceq(instrukcja);
                    case "brfalse":
                        return new Brfalse(instrukcja);
                    case "brfalse.s":
                        return new Brfalse(instrukcja);
                    case "brtrue.s":
                        return new Brtrue(instrukcja);
                    case "constrained.":
                    case "constrained":
                        return new Constrained(instrukcja);
                    case "castclass":
                        return new Castclass(instrukcja);
                    case "isinst":
                        return new Isinst(instrukcja);
                    case "ldtoken":
                        return new Ldtoken(instrukcja);
                    case "newarr":
                        return new Newarr(instrukcja);
                    case "dup":
                        return new Dup(instrukcja);
                    case "stelem.ref":
                        return new Stelem_Ref(instrukcja);
                    case "ldfld":
                        return new Ldfld(instrukcja);
                    case "stfld":
                        return new Stfld(instrukcja);
                    case "cgt.un":
                        return new Cgt_Un(instrukcja);
                    case "conv.r8":
                        return new Conv_R8(instrukcja);
                    case "unbox.any":
                        return new Unbox_Any(instrukcja);
                    case "br":
                        return new Br(instrukcja);
                    case "initobj":
                        return new Initobj(instrukcja);
                    case "box":
                        return new Box(instrukcja);
                    case "unbox":
                        return new Unbox(instrukcja);
                    case "ldsfld":
                        return new Ldsfld(instrukcja);
                    case "ldftn":
                        return new Ldftn(instrukcja);
                    case "stsfld":
                        return new Stsfld(instrukcja);
                    case "mul":
                        return new Mul(instrukcja);
                    case "div":
                        return new Div(instrukcja);
                    case "beq.s":
                        return new Beq(instrukcja);
                }

                throw new Exception("Brak instrukcji " + instrukcja.OpCode.Name + " " + instrukcja.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
