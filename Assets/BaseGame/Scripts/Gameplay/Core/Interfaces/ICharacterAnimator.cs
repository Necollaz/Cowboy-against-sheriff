namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface ICharacterAnimator
    {
        public void PlayIdle();
        
        public void PlayRun();

        public void PlayPlayerAttack();
        
        public void PlayEnemyAttack();
        
        public void PlayDeath();
        
        public void ResetTrigger(int hash);
    }
}