using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Animation
{
    public class CharacterAnimator : ICharacterAnimator
    {
        private readonly Animator _animator;

        public CharacterAnimator(Animator animator) => _animator = animator;

        public void PlayIdle()
        {
            _animator.SetBool(AnimationIDs.Run, false);
        }

        public void PlayRun()
        {
            _animator.SetBool(AnimationIDs.Run, true);
        }

        public void PlayPlayerAttack()
        {
            _animator.SetTrigger(AnimationIDs.AttackPlayer);
        }

        public void PlayEnemyAttack()
        {
            _animator.SetTrigger(AnimationIDs.AttackEnemy);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(AnimationIDs.Death);
        }

        public void ResetTrigger(int hash)
        {
            _animator.ResetTrigger(hash);
        }
    }
}