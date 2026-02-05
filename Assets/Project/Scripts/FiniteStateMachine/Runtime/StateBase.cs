namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public abstract partial class StateBase
    {
        protected StateBase()
        {
        }

        #region Internal Methods

        internal void OnEnterInternal()
        {
            OnEnter();
        }

        internal void OnExitInternal()
        {
            OnExit();
        }
        
        #endregion
        
        #region Virtual Methods

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }
        
        public virtual void Handle(IDispatcher visitor)
        {
            visitor.Dispatch(this);
        }
        #endregion
    }
}