using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public abstract class BaseAnimation : ICharacterAnimator
    {
        protected readonly Animator Animator;
        
        protected BaseAnimation(Animator animator) => Animator = animator;
    
        public virtual void PlayIdle() => Animator.SetBool(AnimationParams.IsRunning, false);
        
        public virtual void PlayRun()  => Animator.SetBool(AnimationParams.IsRunning, true);
        
        public abstract void PlayAttack();
        
        public virtual void PlayDeath() => Animator.SetTrigger(AnimationParams.Death);
    }
}