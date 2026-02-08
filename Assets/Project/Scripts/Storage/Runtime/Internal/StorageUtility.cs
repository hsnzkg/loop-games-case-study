using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts.Storage.Runtime.Internal
{
    public static class StorageUtility
    {
        public static List<Type> GetAssemblies(this Type interfaceType)
        {
            List<Type> implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .ToList(); 
            
            return implementingTypes;
        }
    }
}