namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public interface IStateObject
    {
        public void EnterState();
        
        public void UpdateState();
        
        public void HandleInputPlayer();
        
        public void ExitState();
    }
}