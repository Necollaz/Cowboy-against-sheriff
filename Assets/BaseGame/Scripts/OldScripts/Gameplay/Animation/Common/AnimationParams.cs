using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public static class AnimationParams
    {
        private const string RunParam = "isRunning";
        private const string AttackParam = "Attack";
        private const string AttackMeleeParam = "AttackMelee"; 
        private const string DeathParam = "Death";
        
        public static readonly int IsRunning = Animator.StringToHash(RunParam);
        public static readonly int AttackPlayer = Animator.StringToHash(AttackParam);
        public static readonly int AttackEnemy = Animator.StringToHash(AttackMeleeParam);
        public static readonly int Death = Animator.StringToHash(DeathParam);
    }
}