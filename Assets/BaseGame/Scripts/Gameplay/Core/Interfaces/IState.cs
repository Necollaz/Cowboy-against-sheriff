namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void HandleInput();
        public void LogicUpdate();
        public void Exit();
    }
}