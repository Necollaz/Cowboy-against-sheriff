using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class AnimationIDs
    {
        public static readonly int Run = Animator.StringToHash("isRunning");
        public static readonly int AttackPlayer = Animator.StringToHash("Attack");
        public static readonly int AttackEnemy = Animator.StringToHash("AttackMelee");
        public static readonly int Death = Animator.StringToHash("Death");
    }
}