using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using NeuroSystem.VirtualMachine;
using NeuroSystem.VirtualMachine.Core;
using NeuroSystem.VirtualMachine.Core.Variables;
using NeuroSystem.VirtualMachine.Core.Variables.Value;
using NeuroSystem.VirtualMachine.Instrukcje;
using NeuroSystem.VirtualMachine.Instrukcje.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje
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
        public Code KodInstrukcji { get; set; }
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
            return WirtualnaMaszyna.AktualnaMetoda.LocalArguments.Pobierz(indeks);
        }

        public void ZapiszLokalnaZmienna(object o, int indeks)
        {
            WirtualnaMaszyna.AktualnaMetoda.LocalVariables.Ustaw(indeks, o);
        }

        public object PobierzLokalnaZmienna(int indeks)
        {
            return WirtualnaMaszyna.AktualnaMetoda.LocalVariables.Pobierz(indeks);
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

        /// <summary>
        /// Zwraca obiekt
        /// jeśli jest adres na stosie to zamienia na obiekt
        /// </summary>
        /// <returns></returns>
        public object PopObject()
        {
            var ob =  WirtualnaMaszyna.Stos.Pop();
            if(ob is ObjectWraperBase)
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

        public static InstructionBase UtworzInstrukcje(Instruction instrukcja)
        {
            try
            {

            
            switch (instrukcja.OpCode.Code)
            {
                case Code.Nop:
                    return new Nop(instrukcja);
                case Code.Ldarg_0:
                    return new Ldarg(0, instrukcja);
                case Code.Ldarg_1:
                    return new Ldarg(1, instrukcja);
                case Code.Ldarg_2:
                    return new Ldarg(2, instrukcja);
                case Code.Ldarg_3:
                    return new Ldarg(3, instrukcja);
                case Code.Ldarga_S:
                    return new Ldarga(instrukcja);
                case Code.Starg_S:
                    return new Starg(((Mono.Cecil.ParameterDefinition)instrukcja.Operand).Index, instrukcja);
                case Code.Call:
                    return new Call(instrukcja);
                case Code.Callvirt:
                    return new Callvirt(instrukcja);
                case Code.Ret:
                    return new Ret(instrukcja);
                case Code.Add:
                    return new Add(instrukcja);
                case Code.Stloc_S:
                    return new Stloc(((VariableDefinition)instrukcja.Operand).Index, instrukcja);
                case Code.Stloc_0:
                    return new Stloc(0, instrukcja);
                case Code.Stloc_1:
                    return new Stloc(1, instrukcja);
                case Code.Stloc_2:
                    return new Stloc(2, instrukcja);
                case Code.Stloc_3:
                    return new Stloc(3, instrukcja);
                case Code.Ldloc_S:
                    return new Ldloc(((VariableDefinition)instrukcja.Operand).Index, instrukcja);
                case Code.Ldloc_0:
                    return new Ldloc(0, instrukcja);
                case Code.Ldloc_1:
                    return new Ldloc(1, instrukcja);
                case Code.Ldloc_2:
                    return new Ldloc(2, instrukcja);
                case Code.Ldloc_3:
                    return new Ldloc(3, instrukcja);
                case Code.Br_S:
                    return new Br_S(instrukcja);
                case Code.Sub:
                    return new Sub(instrukcja);
                case Code.Ldstr:
                    return new Ldstr(instrukcja);
                case Code.Newobj:
                    return new Newobj(instrukcja);
                case Code.Throw:
                    return new Throw(instrukcja);
                case Code.Leave_S:
                    return new Leave_S(instrukcja);
                case Code.Endfinally:
                    return new Endfinally(instrukcja);
                case Code.Pop:
                    return new Pop(instrukcja);
                case Code.Ldc_I4_M1:
                    return new Ldc(-1, instrukcja);
                case Code.Ldc_I4_0:
                    return new Ldc(0, instrukcja);
                case Code.Ldc_I4_1:
                    return new Ldc(1, instrukcja);
                case Code.Ldc_I4_2:
                    return new Ldc(2, instrukcja);
                case Code.Ldc_I4_3:
                    return new Ldc(3, instrukcja);
                case Code.Ldc_I4_4:
                    return new Ldc(4, instrukcja);
                case Code.Ldc_I4_5:
                    return new Ldc(5, instrukcja);
                case Code.Ldc_I4_6:
                    return new Ldc(6, instrukcja);
                case Code.Ldc_I4_S:
                    return new Ldc((sbyte)instrukcja.Operand, instrukcja);
                case Code.Ldc_I4:
                    return new Ldc((Int32)instrukcja.Operand, instrukcja);
                case Code.Ldc_R8:
                    return new Ldc(instrukcja.Operand, instrukcja);
                case Code.Ldnull:
                    return new Ldc(null, instrukcja);
                case Code.Ldloca_S:
                    return new Ldloca(instrukcja.Operand, instrukcja);
                case Code.Cgt:
                    return new Cgt(instrukcja);
                case Code.Clt:
                    return new Clt(instrukcja);
                case Code.Ceq:
                    return new Ceq(instrukcja);
                case Code.Brfalse:
                    return new Brfalse(instrukcja);
                case Code.Brfalse_S:
                    return new Brfalse(instrukcja);
                case Code.Brtrue_S:
                    return new Brtrue(instrukcja);
                case Code.Constrained:
                    return new Constrained(instrukcja);
                case Code.Castclass:
                    return new Castclass(instrukcja);
                case Code.Isinst:
                    return new Isinst(instrukcja);
                case Code.Ldtoken:
                    return new Ldtoken(instrukcja);
                case Code.Newarr:
                    return new Newarr(instrukcja);
                case Code.Dup:
                    return new Dup(instrukcja);
                case Code.Stelem_Ref:
                    return new Stelem_Ref(instrukcja);
                case Code.Ldfld:
                    return new Ldfld(instrukcja);
                case Code.Stfld:
                    return new Stfld(instrukcja);
                case Code.Cgt_Un:
                    return new Cgt_Un(instrukcja);
                case Code.Conv_R8:
                    return new Conv_R8(instrukcja);
                case Code.Unbox_Any:
                    return new Unbox_Any(instrukcja);
                case Code.Br:
                    return new Br(instrukcja);
                case Code.Initobj:
                    return new Initobj(instrukcja);
                    case Code.Box:
                    return new Box(instrukcja);
                    case Code.Unbox:
                    return new Unbox(instrukcja);


            }

            throw new Exception("Brak instrukcji " + instrukcja.OpCode.Code + " " + instrukcja.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
