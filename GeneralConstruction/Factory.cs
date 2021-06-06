using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralConstruction
{
    public class Factory
    {
        private readonly Type[] _assemblyTypes;
        public Factory(Assembly assembly)
        {
            _assemblyTypes = assembly.GetTypes();
        }

        public Factory()
        {
            _assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
        }

        public List<string> GetTypeNames<TParent>() => EnumerateTypeNames<TParent>().ToList();

        public TInterface CreateInstance<TInterface>(string typeName)
        {
            var type = GetType(typeof(TInterface), typeName);
            return (TInterface)Activator.CreateInstance(type);
        }

        public Type GetType(Type parentType, string typeName)
        {
            var interfaceName = GetInterfaceName(parentType);
            foreach (var type in GetChildTypes(parentType))
            {
                if (type.Name == typeName) return type;
                if (TrimInterfaceName(type.Name, interfaceName) == typeName) return type;
            }

            return null;
        }

        private string TrimInterfaceName(string typeName, string interfaceName)
        {
            if (typeName.EndsWith(interfaceName))
            {
                typeName = typeName.Remove(typeName.Length - interfaceName.Length);
            }
            return typeName;
        }

        private IEnumerable<Type> GetChildTypes(Type parentType) =>
            _assemblyTypes.Where(t => t != parentType).Where(parentType.IsAssignableFrom);

        private IEnumerable<string> EnumerateTypeNames<TParent>(bool removeInterfaceName = true) 
        {
            var parentType = typeof(TParent);
            var interfaceName = GetInterfaceName(parentType);
            foreach (var type in GetChildTypes(parentType))
            {
                var name = type.Name;
                if (removeInterfaceName)
                {
                    name = TrimInterfaceName(name, interfaceName);
                }

                yield return name;
            }
        }

        private string GetInterfaceName(Type typeOfInterface)
        {
            var name = typeOfInterface.Name;
            if (name.StartsWith("I")) name = name.Remove(0, 1);
            return name;
        }

    }
}
