using UnityEngine;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface IAttackCooldown
    {
        public bool CanAttack { get; }
        
        public void Initialize(Animator animator, float attackRange, float damage, float attackDuration);
        
        public void TryAttack(Transform attacker, HealthComponent targetHealth);

        public void UpdateCooldown();
    }
}