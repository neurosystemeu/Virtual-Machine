using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Biznesowe;
using NeuroSystem.UnitTestVirtualMachine.Klasy.Procesy;

namespace NeuroSystem.UnitTestVirtualMachine.Procesy.Fluent
{
    public class FluentUIProcess : Proces
    {
        public object Wynik { get; set; }

        public override object Start()
        {
            var view = DataFormView<Person>();
            view.DataForm(df =>
            {
                df.AddField(p => p.Name);
                df.AddField(p => p.Surname);
                df.AddField(p => p.Age);
            });

            Wynik = view;

            return "OK";
        }

        public DataFormFactory<T> DataFormView<T>()
        {
            var view = new DataFormFactory<T>();

            return view;
        }
    }
}
