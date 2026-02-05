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
        private Vector2 _scrollPosition;
        private static IReadOnlyList<Type> _lastKnownEventTypes;
        private readonly Dictionary<Type, EventBusData> _lastKnownData = new();
        private readonly Dictionary<Type, bool> _expandedStates = new();
        
        private GUIStyle _outerBoxStyle;
        private GUIStyle _handlerBoxStyle;
        private GUIStyle _richMiniBoldStyle;
        private GUIStyle _foldoutStyle;

        [MenuItem("Project/Event Bus Inspector")]
        public static void ShowWindow()
        {
            _lastKnownEventTypes = EventBusUtility.GetRegisteredBusTypes();
            GetWindow<EventBusInspector>("Event Bus Inspector");
        }

        private void OnGUI()
        {
            InitializeGUIStyle();

            GUILayout.Label("📡 Event Buses", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (_lastKnownEventTypes == null || _lastKnownEventTypes.Count == 0)
            {
                GUILayout.Label("📡 Event types not found !");
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            foreach (Type eventType in _lastKnownEventTypes)
            {
                DrawEventTypeBox(eventType);
                EditorGUILayout.Space(10);
            }

            EditorGUILayout.EndScrollView();
        }

        private void InitializeGUIStyle()
        {
            _outerBoxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(8, 8, 6, 6),
                margin = new RectOffset(0, 0, 0, 0)
            };

            _handlerBoxStyle = new GUIStyle("box")
            {
                padding = new RectOffset(4, 4, 2, 2),
                margin = new RectOffset(4, 4, 2, 2)
            };

            _richMiniBoldStyle = new GUIStyle(EditorStyles.miniBoldLabel)
            {
                richText = true
            };
            
            _foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                fontStyle = FontStyle.Bold
            };
        }

        private void DrawEventTypeBox(Type eventType)
        {
            if (!_expandedStates.TryGetValue(eventType, out bool expanded)) expanded = false;

            BeginBorderedBox(_outerBoxStyle);

            expanded = DrawEventFoldout(eventType, expanded);
            _expandedStates[eventType] = expanded;

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
                _foldoutStyle
            );

            return expanded;
        }

        private static bool TryGetBindings(Type eventType, out IList bindings)
        {
            bindings = null;

            FieldInfo field = eventType.GetField("_bindings",
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
                GetDelegate(type, binding, "_handlerWithArgs") ??
                GetDelegate(type, binding, "_handlerNoArgs");

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
            BeginBorderedBox(_handlerBoxStyle);
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
                    _richMiniBoldStyle))
            {
                onClick?.Invoke();
            }

            GUILayout.EndHorizontal();
        }

        private void DrawInfoRow(string icon, string label, string value, string color)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(icon, GUILayout.Width(20));
            GUILayout.Label($"<color=#4AA5F0>{label} :</color> <color={color}>{value}</color>", _richMiniBoldStyle);
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

            if (!ReferenceEquals(currentTypes, _lastKnownEventTypes))
            {
                _lastKnownEventTypes = currentTypes;
                shouldRepaint = true;
            }

            if (_lastKnownEventTypes == null)
                return;

            foreach (Type eventType in _lastKnownEventTypes)
            {
                FieldInfo field = eventType.GetField("_bindings", BindingFlags.Static | BindingFlags.NonPublic);
                if (field == null) continue;

                IList currentBindings = field.GetValue(null) as IList;
                if (currentBindings == null) continue;

                if (!_lastKnownData.TryGetValue(eventType, out EventBusData cached))
                {
                    _lastKnownData[eventType] = new EventBusData(currentBindings);
                    shouldRepaint = true;
                    continue;
                }

                if (cached.HasChanged(currentBindings))
                {
                    _lastKnownData[eventType] = new EventBusData(currentBindings);
                    _expandedStates[eventType] = true;
                    shouldRepaint = true;
                }
            }

            if (shouldRepaint)
                Repaint();
        }
    }
}