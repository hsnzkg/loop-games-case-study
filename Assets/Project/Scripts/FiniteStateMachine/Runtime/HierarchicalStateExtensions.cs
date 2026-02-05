namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public static class HierarchicalStateExtensions
    {
        public static void AddState(this StateBase parent, StateBase state)
        {
            if (!parent.GetIsHierarchical()) parent.ConvertHierarchical();
            state.Parent = parent;
            parent.GetStateMachine().AddState(state);
        }

        public static void SetDefaultState<T>(this StateBase state)
        {
            state.GetStateMachine().SetDefaultState<T>();
        }
        
        public static void ChangeRuntimeMode(this StateBase state, IRuntimeMode mode)
        {
            state.GetStateMachine()?.SetRuntimeMode(mode);
        }
        
        public static StateBase GetState<T>(this StateBase state)
        {
            return state.GetStateMachine().GetState<T>();
        }

        public static StateBase GetActiveState(this StateBase state)
        {
            return state.GetStateMachine().GetActiveState();
        }

        public static StateBase GetLeafState(this StateBase state)
        {
            return state.GetStateMachine()?.GetLeafState();
        }

        public static void SetDefaultState(this StateBase parent, StateBase child)
        {
            parent.GetStateMachine().SetDefaultState(child);
        }
    }
}