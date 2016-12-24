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

        public static Type GetSystemType(this TypeReference typeReference)
        {
            var genericInstanceType = typeReference as GenericInstanceType;
            var nazwa = typeReference.Name;
            var przestrzenNazw = typeReference.Namespace;
            var pelnaNazwa = typeReference.FullName.Replace("/", "+");
            var isNested = typeReference.IsNested;

            if (genericInstanceType == null)
            {
                //mamy zwykły typ - sprawdzam tylko nazwę

                var assembly = typeReference.Scope.GetSystemAssembly();
                var typyWewnetrzne = assembly.GetTypes().Where(t => t.IsNested == isNested);
                return typyWewnetrzne.Single(t => t.FullName == pelnaNazwa);
            }
            else
            {
                // mamy generika

                var assembly = genericInstanceType.Scope.GetSystemAssembly();
                //typ bazowy
                var typy = assembly.GetTypes().Where(t => t.ContainsGenericParameters && t.IsNested == isNested);

                Type typBazowy;
                if (isNested)
                {
                    var pelnaNazwaWew = pelnaNazwa.Split('<')[0]; //Generyk wewnętrzny ma po < typ parametru generycznego >
                    typBazowy = typy.Single(t => t.FullName == pelnaNazwaWew);
                }
                else
                {
                    typBazowy = typy.Single(t => t.Name == nazwa && t.Namespace == przestrzenNazw);
                }


                var listaTypowParametrowGenerycznych = new List<Type>();
                foreach (var genericArgument in genericInstanceType.GenericArguments)
                {
                    var typ = genericArgument.GetSystemType();
                    listaTypowParametrowGenerycznych.Add(typ);
                }

                var typSystemowy = typBazowy.MakeGenericType(listaTypowParametrowGenerycznych.ToArray());
                return typSystemowy;
            }
        }

        public static Assembly GetSystemAssembly(this IMetadataScope scope)
        {
            var assemblyName = scope as AssemblyNameReference;
            if (assemblyName != null)
            {
                return Assembly.Load(assemblyName.FullName);
            }
            else
            {
                var moduleDefinition = scope as ModuleDefinition;
                if (moduleDefinition != null)
                {
                    return Assembly.LoadFile(moduleDefinition.FullyQualifiedName);
                }
            }

            throw new NotImplementedException();
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

        public static Assembly GetSystemAssembly(this ModuleDefinition module)
        {
            return Assembly.LoadFile(module.FullyQualifiedName);
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
