using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyMeleeAttacker : MonoBehaviour
    {
        [SerializeField] private float _attackDamage = 10f;

        private MeleeAttacker _attacker;
        private HealthComponent _playerHealth;

        public bool CanAttack => _attacker.CanAttack;

        private void Awake()
        {
            _attacker = new MeleeAttacker();
        }

        public void Initialize(Animator animator, float attackRange, HealthComponent playerHealth)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "AttackEnemy");

            float duration = clip?.length ?? 1f;

            _attacker.Initialize(animator, attackRange, _attackDamage, duration);
            _playerHealth = playerHealth;
        }

        public void Tick(Transform self)
        {
            _attacker.UpdateCooldown();

            if (_attacker.CanAttack)
                _attacker.TryAttack(self, _playerHealth);
        }
    }
}