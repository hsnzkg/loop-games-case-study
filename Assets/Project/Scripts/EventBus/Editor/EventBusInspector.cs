using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Project.Scripts.EventBus.Runtime;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.EventBus.Editor
{
    public class EventBusInspector : EditorWindow
    {
        private Vector2 m_scrollPosition;
        private static IReadOnlyList<Type> s_lastKnownEventTypes;
        private readonly Dictionary<Type, EventBusData> m_lastKnownData = new Dictionary<Type, EventBusData>();
        private readonly Dictionary<Type, bool> m_expandedStates = new Dictionary<Type, bool>();
        
        private GUIStyle m_outerBoxStyle;
        private GUIStyle m_handlerBoxStyle;
        private GUIStyle m_richMiniBoldStyle;
        private GUIStyle m_foldoutStyle;

        [MenuItem("Project/Event Bus Inspector")]
        public static void ShowWindow()
        {
            s_lastKnownEventTypes = EventBusUtility.GetRegisteredBusTypes();
            GetWindow<EventBusInspector>("Event Bus Inspector");
        }

        private void OnGUI()
        {
            InitializeGUIStyle();

            GUILayout.Label("📡 Event Buses", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (s_lastKnownEventTypes == null || s_lastKnownEventTypes.Count == 0)
            {
                GUILayout.Label("📡 Event types not found !");
                return;
            }

            m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

            foreach (Type eventType in s_lastKnownEventTypes)
            {
                DrawEventTypeBox(eventType);
                EditorGUILayout.Space(10);
            }

            EditorGUILayout.EndScrollView();
        }

        private void InitializeGUIStyle()
        {
            m_outerBoxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(8, 8, 6, 6),
                margin = new RectOffset(0, 0, 0, 0)
            };

            m_handlerBoxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(4, 4, 2, 2),
                margin = new RectOffset(4, 4, 2, 2)
            };

            m_richMiniBoldStyle = new GUIStyle(EditorStyles.miniBoldLabel)
            {
                richText = true
            };
            
            m_foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                fontStyle = FontStyle.Bold
            };
        }

        private void DrawEventTypeBox(Type eventType)
        {
            if (!m_expandedStates.TryGetValue(eventType, out bool expanded)) expanded = false;

            BeginBorderedBox(m_outerBoxStyle);

            expanded = DrawEventFoldout(eventType, expanded);
            m_expandedStates[eventType] = expanded;

            if (!expanded)
            {
                EndBorderedBox();
                return;
            }

            if (!TryGetBindings(eventType, out IList bindings))
            {
                EndBorderedBox();
                return;
            }

            EditorGUILayout.Space(4);

            foreach (object binding in bindings)
            {
                if (!TryResolveHandlerInfo(binding, out HandlerInfo info)) 
                    continue;

                DrawHandlerBox(info);
            }

            EndBorderedBox();
        }
        
        private bool DrawEventFoldout(Type eventType, bool expanded)
        {
            expanded = EditorGUILayout.Foldout(
                expanded,
                $"🚨 {eventType.GetFriendlyTypeName()}",
                true,
                m_foldoutStyle
            );

            return expanded;
        }

        private static bool TryGetBindings(Type eventType, out IList bindings)
        {
            bindings = null;

            FieldInfo field = eventType.GetField("s_bindings",
                BindingFlags.Static | BindingFlags.NonPublic);

            if (field == null)
            {
                GUILayout.Label("No bindings field found.");
                return false;
            }

            bindings = field.GetValue(null) as IList;
            if (bindings == null)
            {
                GUILayout.Label("No bindings data found.");
                return false;
            }

            return true;
        }


        private static bool TryResolveHandlerInfo(object binding, out HandlerInfo info)
        {
            info = default;

            Type type = binding.GetType();

            Delegate handler =
                GetDelegate(type, binding, "m_handlerWithArgs") ??
                GetDelegate(type, binding, "m_handlerNoArgs");

            if (handler == null || handler.GetInvocationList().Length == 0)
                return false;

            Delegate first = handler.GetInvocationList()[0];

            info = new HandlerInfo
            {
                Handler = first,
                MethodName = first.Method.Name,
                TargetName = first.Target != null
                    ? first.Target.GetType().GetFriendlyTypeName()
                    : first.Method.DeclaringType.GetFriendlyTypeName(),
                Priority = GetPriority(type, binding)
            };

            return true;
        }

        private static Delegate GetDelegate(Type type, object obj, string fieldName)
        {
            return type
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(obj) as Delegate;
        }

        private static EventPriority GetPriority(Type type, object obj)
        {
            FieldInfo field = type.GetField("Priority",
                BindingFlags.Instance | BindingFlags.NonPublic);

            return field != null
                ? (EventPriority)field.GetValue(obj)
                : EventPriority.MONITOR;
        }


        private void DrawHandlerBox(HandlerInfo info)
        {
            BeginBorderedBox(m_handlerBoxStyle);
            DrawClickableRow("👤", "Target", info.TargetName, () => PingScript(info.Handler?.Target?.GetType() ?? info.Handler?.Method?.DeclaringType));
            DrawClickableRow("🔧", "Method", info.MethodName, () => OpenMethod(info.Handler?.Method));
            DrawInfoRow("⚡", "Priority", info.Priority.ToString(), "#FFD700");
            EndBorderedBox();
        }


        private void DrawClickableRow(string icon, string label, string value, Action onClick)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(icon, GUILayout.Width(20));

            if (GUILayout.Button(
                    $"<color=#4AA5F0>{label} :</color> <color=#6CC24A>{value}</color>",
                    m_richMiniBoldStyle))
            {
                onClick?.Invoke();
            }

            GUILayout.EndHorizontal();
        }

        private void DrawInfoRow(string icon, string label, string value, string color)
        {
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(icon, GUILayout.Width(20));
            GUILayout.Label($"<color=#4AA5F0>{label} :</color> <color={color}>{value}</color>", m_richMiniBoldStyle);
            GUILayout.EndHorizontal();
        }

        private static void PingScript(Type type)
        {
            if (type == null) return;

            MonoScript script = type.FindScriptFromType();
            if (script == null) return;

            EditorGUIUtility.PingObject(script);
            AssetDatabase.OpenAsset(script);
        }

        private static void OpenMethod(MethodInfo method)
        {
            if (method == null) return;

            MonoScript script = method.DeclaringType.FindScriptFromType();
            if (script == null) return;

            int line = script.GetMethodLine(method);
            EditorGUIUtility.PingObject(script);
            AssetDatabase.OpenAsset(script, line);
        }

        private void BeginBorderedBox(GUIStyle contentStyle)
        {
            Rect rect = EditorGUILayout.BeginVertical();

            Handles.BeginGUI();
            Handles.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            Handles.DrawAAPolyLine(
                1.5f,
                new Vector3(rect.xMin, rect.yMin),
                new Vector3(rect.xMax, rect.yMin),
                new Vector3(rect.xMax, rect.yMax),
                new Vector3(rect.xMin, rect.yMax),
                new Vector3(rect.xMin, rect.yMin));
            Handles.EndGUI();

            GUILayout.BeginVertical(contentStyle);
        }

        private void EndBorderedBox()
        {
            GUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }
        
        private void Update()
        {
            bool shouldRepaint = false;

            IReadOnlyList<Type> currentTypes = EventBusUtility.GetRegisteredBusTypes();

            if (!ReferenceEquals(currentTypes, s_lastKnownEventTypes))
            {
                s_lastKnownEventTypes = currentTypes;
                shouldRepaint = true;
            }

            if (s_lastKnownEventTypes == null)
                return;

            foreach (Type eventType in s_lastKnownEventTypes)
            {
                FieldInfo field = eventType.GetField("m_bindings", BindingFlags.Static | BindingFlags.NonPublic);
                if (field == null) continue;

                if (field.GetValue(null) is not IList currentBindings) continue;

                if (!m_lastKnownData.TryGetValue(eventType, out EventBusData cached))
                {
                    m_lastKnownData[eventType] = new EventBusData(currentBindings);
                    shouldRepaint = true;
                    continue;
                }

                if (cached.HasChanged(currentBindings))
                {
                    m_lastKnownData[eventType] = new EventBusData(currentBindings);
                    m_expandedStates[eventType] = true;
                    shouldRepaint = true;
                }
            }

            if (shouldRepaint)
                Repaint();
        }
    }
}