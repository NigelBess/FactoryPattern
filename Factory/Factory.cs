using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralConstruction
{
    public class Factory
    {
        private readonly Assembly _assembly;
        private readonly Type[] _assemblyTypes;
        public Factory(Assembly assembly)
        {
            _assemblyTypes = assembly.GetTypes();
        }

        public List<string> GetTypeNames<TParent>() => EnumerateTypeNames<TParent>().ToList();


        private IEnumerable<string> EnumerateTypeNames<TParent>() 
        {
            var parentType = typeof(TParent);
            foreach (var type in _assemblyTypes.Where(t => t.IsAssignableFrom(parentType)))
            {
                yield return type.Name;
            }
        }

    }
}
