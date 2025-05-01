namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public interface ICharacterAnimator
    {
        public void PlayIdle();
        
        public void PlayRun();

        public void PlayAttack();
        
        public void PlayDeath();
    }
}