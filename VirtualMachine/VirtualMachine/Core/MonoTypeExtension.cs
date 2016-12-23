using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSystem.VirtualMachine.Klasy
{
    public static class MonoTypeExtension
    {
        #region Mono.Cecil => System.Type

        public static Type GetSystemType(this TypeReference type)
        {
            var nazwaTypu = type.GetReflectionName();
            var typ = Type.GetType(nazwaTypu, f => GetAssemblyByName(f), (assem, name, ignore) => GetTypeBy(assem, name, ignore), true);

            return typ;
        }

        private static string GetReflectionName(this TypeReference type)
        {
            if (type.IsGenericInstance)
            {
                var genericInstance = (GenericInstanceType)type;
                return string.Format("{0}.{1}[{2}]", genericInstance.Namespace, type.Name, String.Join(",", genericInstance.GenericArguments.Select(p => p.GetReflectionName()).ToArray()));
            }
            return type.FullName;
        }

        public static Type GetTypeBy(Assembly assem, string name, bool ignore)
        {
            var a = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in a)
            {
                var t = assembly.GetType(name);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public static Assembly GetAssemblyByName(AssemblyName assemblyName)
        {
            return null;
        }

        #endregion


        #region System.Type to Modo.Cecil

        public static TypeDefinition GetTypeDefinition(this Type typ)
        {
            var module = ModuleDefinition.ReadModule(typ.Assembly.Location);
            var im = module.Import(typ);
            var td = im.Resolve();

            return td;
        }
        #endregion


    }
}
