using System;
using System.Collections.Generic;
using System.Reflection;
using Project.Scripts.Storage.Runtime;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Storage.Editor
{
    public class StorageInspectorWindow : EditorWindow
    {
        private Vector2 m_scrollPosition;
        private static List<Type> s_lastKnownStorageTypes;
        private readonly Dictionary<Type, StorageData> m_lastKnownData = new Dictionary<Type, StorageData>();

        [MenuItem("Project/Storage Inspector")]
        public static void ShowWindow()
        {
            s_lastKnownStorageTypes = GetRegisteredStorageTypes();
            GetWindow<StorageInspectorWindow>("Storage Inspector");
        }

        private void OnGUI()
        {
            GUILayout.Label("🗄️Storages", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (s_lastKnownStorageTypes == null || s_lastKnownStorageTypes.Count == 0)
            {
                GUILayout.Label("🗄️Storage types not found !");
                return;
            }

            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

            foreach (Type type in s_lastKnownStorageTypes)
            {
                object instance = GetStorageInstance(type);

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"📦 Storage<{type.Name}>", EditorStyles.boldLabel);
                EditorGUILayout.EndHorizontal();

                if (instance == null)
                {
                    EditorGUILayout.HelpBox("Storage has not been created.", MessageType.Info);
                }
                else
                {
                    FieldInfo[] fields = GetStorageFields(type);
                    if (fields.Length == 0)
                    {
                        EditorGUILayout.HelpBox("No public fields found in this storage.", MessageType.Info);
                    }
                    else
                    {
                        foreach (FieldInfo field in fields)
                        {
                            object fieldValue = GetFieldValue(field, instance);
                            bool isNull = fieldValue == null;

                            GUIStyle fieldBoxStyle = new("box")
                            {
                                normal =
                                {
                                    background = MakeTexture(1, 1, new Color(0, 0, 0, 0.25f))
                                },
                                padding = new RectOffset(6, 6, 3, 3)
                            };

                            EditorGUILayout.BeginVertical(fieldBoxStyle);
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(field.Name, GUILayout.Width(200));
                            EditorGUILayout.LabelField(isNull ? "Null" : fieldValue.ToString(), isNull ? EditorStyles.boldLabel : EditorStyles.label);
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.EndVertical();
                        }
                    }
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndScrollView();
        }

        private void Update()
        {
            bool shouldRepaint = false;
            List<Type> types = GetRegisteredStorageTypes();
            if (!Equals(types, s_lastKnownStorageTypes))
            {
                s_lastKnownStorageTypes = types;
                shouldRepaint = true;
            }

            if (s_lastKnownStorageTypes is { Count: > 0 })
            {
                foreach (Type type in s_lastKnownStorageTypes)
                {
                    object instance = GetStorageInstance(type);
                    if (!m_lastKnownData.TryGetValue(type, out StorageData storageData))
                    {
                        storageData = new StorageData(instance);
                        m_lastKnownData[type] = storageData;
                        shouldRepaint = true;
                    }
                    else
                    {
                        if (storageData.Instance != instance)
                        {
                            storageData = new StorageData(instance);
                            m_lastKnownData[type] = storageData;
                            shouldRepaint = true;
                        }
                        else
                        {
                            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                            foreach (FieldInfo field in fields)
                            {
                                object currentValue = GetFieldValue(field, instance);
                                if (storageData.FieldValues.TryGetValue(field.Name, out object lastValue) && Equals(lastValue, currentValue))
                                    continue;
                                
                                storageData.FieldValues[field.Name] = currentValue;
                                shouldRepaint = true;
                            }
                        }
                    }
                }
            }
            if (shouldRepaint)
                Repaint();
        }

        private static Texture2D MakeTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D texture = new(width, height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        private static List<Type> GetRegisteredStorageTypes()
        {
            Type storageCenterType = typeof(StorageCenter);
            FieldInfo field = storageCenterType.GetField("s_registeredStorageTypes", BindingFlags.Static | BindingFlags.NonPublic);
            return field?.GetValue(null) as List<Type>;
        }

        private static FieldInfo[] GetStorageFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        }

        private static object GetFieldValue(FieldInfo field, object instance)
        {
            return field.GetValue(instance);
        }

        private static object GetStorageInstance(Type type)
        {
            Type storageType = typeof(Storage<>).MakeGenericType(type);
            FieldInfo instanceField = storageType.GetField("s_instance", BindingFlags.Static | BindingFlags.NonPublic);
            return instanceField?.GetValue(null);
        }
    }
}