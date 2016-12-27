using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.VirtualMachine.Core;
using NeuroSystem.VirtualMachine.Klasy;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    public class Callvirt : Call
    {
        public Callvirt(Instruction instrukcja) : base(instrukcja)
        {
        }

        //public override void Wykonaj()
        //{
        //    //////////////////////////////////
        //    //Dane funkcji bazowej
        //    var mr = instrukcja.Operand as MethodReference;
        //    if (mr.FullName.Contains("VirtualMachine::Hibernate()"))
        //    {
        //        //wywołał metodę do hibernacji wirtualnej maszyny
        //        WirtualnaMaszyna.HibernujWirtualnaMaszyne();
        //        return;
        //    }
        //    var md = mr.Resolve();
            
        //    var staraMetoda = WirtualnaMaszyna.AktualnaMetoda;
                    
        //    //pobieram argumenty ze stosu i kładę do zmiennych funkcji      
        //    int iloscArgumentow = md.Parameters.Count;
        //    if (md.HasThis)
        //    {
        //        iloscArgumentow++;//+1 to zmienna this
        //    }

        //    //pobieram wlasciwa instancje obiektu ktorego metode wykonamy
        //    var instancja = PobierzElementZeStosu(iloscArgumentow-1);

        //    var typ = instancja.GetType();
        //    var typDef = typ.GetTypeDefinition();
        //    //var module = ModuleDefinition.ReadModule(typ.Module.FullyQualifiedName);
        //    //var typDef = module.Types.FirstOrDefault(t => t.FullName == typ.FullName);
        //    //if (typDef == null)
        //    //{
        //    //    typDef = module.Import(typ).Resolve();
        //    //}
        //    //

        //    var nazwaMetodyBazowej = md.Name;
        //    var metoda = typDef.Methods.FirstOrDefault(mm => mm.Name == nazwaMetodyBazowej);

        //    //sprawdzam czy getter lub seter lub czy metoda lub klasa oznaczona atrybutem Interpertuj
        //    if (CzyWykonacCzyInterpretowac(md) == true)
        //    {
        //        //wykonujemy
        //        //WykonajMetode(mr, instancja);
        //    }
        //    else
        //    {
        //        //interpretujemy
        //        var m = new Metoda();
        //        m.NazwaTypu = typDef.FullName;
        //        m.NazwaMetody = nazwaMetodyBazowej; //to będzie już uruchomienie na właściwym obiekcie
        //        m.AssemblyName = typDef.FullName;
        //        m.NumerWykonywanejInstrukcji = 0;

        //        WirtualnaMaszyna.AktualnaMetoda = m;

        //        WczytajLokalneArgumenty(iloscArgumentow);

        //        //zapisuję aktualną metodę na stosie
        //        PushObject(staraMetoda);
        //    }                
        //}
        
    }
}
