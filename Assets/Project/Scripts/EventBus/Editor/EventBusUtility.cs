using System;
using System.Collections.Generic;
using System.Reflection;
using Project.Scripts.EventBus.Runtime;
using UnityEditor;

namespace Project.Scripts.EventBus.Editor
{
    public static class EventBusUtility
    {
        public static int GetMethodLine(this MonoScript script, MethodInfo method)
        {
            if (script == null || method == null)
                return 0;

            string[] lines = script.text.Split('\n');
            string methodName = method.Name;
            string[] paramTypeNames = Array.ConvertAll(method.GetParameters(), p => p.ParameterType.Name);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (!line.Contains(methodName) || !line.Contains("(") || !line.Contains(")")) continue;
                if (!line.Contains($" {methodName}(") && !line.Contains($"{methodName}("))
                    continue;
                int openParenIndex = line.IndexOf('(');
                int closeParenIndex = line.IndexOf(')', openParenIndex);
                if (openParenIndex <= 0 || closeParenIndex <= openParenIndex) continue;
                string paramSegment = line.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
                bool match = true;
                foreach (string param in paramTypeNames)
                {
                    if (paramSegment.Contains(param)) continue;
                    match = false;
                    break;
                }

                if (match)
                    return i + 1;
            }
            return 0;
        }

        
        public static MonoScript FindScriptFromType(this Type type)
        {
            string[] guids = AssetDatabase.FindAssets("t:MonoScript");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (script != null && script.GetClass() == type)
                {
                    return script;
                }
            }

            return null;
        }
        
        
        public static IReadOnlyList<Type> GetRegisteredBusTypes()
        {
            Type centerType = typeof(EventBusCenter);
            FieldInfo field = centerType.GetField("s_registeredEventTypes", BindingFlags.NonPublic | BindingFlags.Static);

            if (field == null)
                return null;

            return field.GetValue(null) as IReadOnlyList<Type>;
        }

        public static string GetFriendlyTypeName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            string typeName = type.Name;
            int backtickIndex = typeName.IndexOf('`');
            if (backtickIndex > 0)
                typeName = typeName[..backtickIndex];

            Type[] genericArgs = type.GetGenericArguments();
            string genericArgsString = string.Join(", ", Array.ConvertAll(genericArgs, GetFriendlyTypeName));
            return genericArgsString;
            //return $"{typeName}<{genericArgsString}>";
        }
    }
}