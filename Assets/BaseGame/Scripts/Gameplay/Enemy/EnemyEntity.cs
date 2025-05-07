using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Core.Interfaces;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(EnemyRangeMover))]
    [RequireComponent(typeof(EnemyMeleeAttacker))]
    [RequireComponent(typeof(EnemyHealthPresenter))]
    public class EnemyEntity : MonoBehaviour, IEnemyEntity
    {
        private EnemyRangeMover _rangeMover;
        private EnemyMeleeAttacker _meleeAttacker;
        private EnemyHealthPresenter _healthPresenter;
        private HealthComponent _playerHealth;
        private HealthComponent _selfHealth;
        private Transform _playerTransform;
        private Animator _animator;

        public bool IsDead => _selfHealth.IsDead;
        public event Action<IEnemyEntity> Death;

        private void Awake()
        {
            _selfHealth = GetComponent<HealthComponent>();
            _animator = GetComponent<Animator>();
            _rangeMover = GetComponent<EnemyRangeMover>();
            _meleeAttacker = GetComponent<EnemyMeleeAttacker>();
            _healthPresenter = GetComponent<EnemyHealthPresenter>();

            _healthPresenter.Death += OnDeathInternal;
        }

        private void Update()
        {
            if (_playerTransform == null)
                return;

            _rangeMover.Tick();

            if (_rangeMover.IsInRange)
            {
                _meleeAttacker.Tick(transform);
            }
        }

        private void OnDestroy()
        {
            _healthPresenter.Death -= OnDeathInternal;

            if (_playerHealth != null)
                _playerHealth.Death -= OnPlayerDeath;
        }

        public void Initialize(Transform playerTransform, HealthComponent playerHealth)
        {
            _playerTransform = playerTransform;
            _playerHealth = playerHealth;

            _rangeMover.Initialize(playerTransform);

            _meleeAttacker.Initialize(_animator, _rangeMover.AttackRange, _playerHealth);

            _playerHealth.Death += OnPlayerDeath;
        }

        public void Activate()
        {
            _rangeMover.enabled = true;
            _meleeAttacker.enabled = true;
        }

        public void OnAttackHit()
        {
            if (!_playerHealth.IsDead)
                _playerHealth.TakeDamage(_meleeAttacker.CanAttack ? 10f : 0f);
        }
        
        private void OnDeathInternal()
        {
            Death?.Invoke(this);
        }

        private void OnPlayerDeath()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}