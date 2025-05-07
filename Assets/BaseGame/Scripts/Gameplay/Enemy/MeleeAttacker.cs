using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    public class MeleeAttacker : IAttackCooldown
    {
        private Animator _animator;
        private float _damage;
        private float _cooldown;
        private float _timer;

        public bool CanAttack => _timer <= 0f;

        public void Initialize(Animator animator, float attackRange, float damage, float attackDuration)
        {
            _animator = animator;
            _damage = damage;
            _cooldown = attackDuration;
            _timer = 0f;
        }

        public void TryAttack(Transform attacker, HealthComponent targetHealth)
        {
            if (_timer > 0f) 
                return;
            
            _animator.SetTrigger(AnimationIDs.AttackEnemy);
            _timer = _cooldown;
        }

        public void UpdateCooldown()
        {
            if (_timer > 0f)
                _timer -= Time.deltaTime;
        }
    }
}