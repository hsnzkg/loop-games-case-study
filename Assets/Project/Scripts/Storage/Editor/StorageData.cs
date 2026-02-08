using System.Collections.Generic;

namespace Project.Scripts.Storage.Editor
{
    public struct StorageData
    {
        public readonly object Instance;
        public readonly Dictionary<string, object> FieldValues;

        public StorageData(object instance)
        {
            Instance = instance;
            FieldValues = new Dictionary<string, object>();
        }
    }
}