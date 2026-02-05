using System;
using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.EventBus.Editor
{
    public struct HandlerInfo
    {
        public string TargetName;
        public string MethodName;
        public EventPriority Priority;
        public Delegate Handler;
    }
}