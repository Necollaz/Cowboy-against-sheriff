namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public abstract class State
    {
        protected readonly StateMachineContext StateMachineContext;

        protected State(StateMachineContext context)
        {
            StateMachineContext = context;
        }

        public virtual void Enter()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}