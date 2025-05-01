using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;

namespace BaseGame.Scripts.Gameplay.Animation
{
    public class EnemyAnimationAttack : BaseAnimation
    {
        public EnemyAnimationAttack(Animator anim) : base(anim)
        {
        }

        public override void PlayAttack()
        {
            Animator.SetTrigger(AnimationParams.AttackEnemy);
        }
    }
}