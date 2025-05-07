using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class AnimationIDs
    {
        private const string RunParam = "isRunning";
        private const string AttackPlayerParam = "Attack";
        private const string AttackEnemyParam = "AttackMelee";
        private const string DeatParam = "Death";
        
        public static readonly int Run = Animator.StringToHash(RunParam);
        public static readonly int AttackPlayer = Animator.StringToHash(AttackPlayerParam);
        public static readonly int AttackEnemy = Animator.StringToHash(AttackEnemyParam);
        public static readonly int Death = Animator.StringToHash(DeatParam);
    }
}