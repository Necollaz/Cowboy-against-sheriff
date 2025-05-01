using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;

namespace BaseGame.Scripts.Gameplay.Animation
{
    public class PlayerAnimationAttack : BaseAnimation
    {
        public PlayerAnimationAttack(Animator anim) : base(anim)
        {
        }
        
        public override void PlayAttack()
        {
            Animator.SetTrigger(AnimationParams.AttackPlayer);
        }
    }
}