using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Health;
using BaseGame.Scripts.Gameplay.Ragdoll;
using UnityEngine.Serialization;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(RagdollEnabler))]
    [RequireComponent(typeof(HealthComponent))]
    public class EnemyEntity : MonoBehaviour, IEnemyEntity
    {
        private const string AttackName = "AttackEnemy";

        [FormerlySerializedAs("_healthBarPrefab")] [SerializeField] private HealthBarView _healthBarViewPrefab;
        [SerializeField] private float _barHeight = 2f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackDamage = 10f;

        private RangeFollower _iMovement;
        private MeleeAttacker _iAttackCooldown;
        private RagdollEnabler _ragdoll;
        private HealthComponent _healthEnemy;
        private HealthBarView _healthBarViewInstance;
        private HealthLifecycle _healthLifecycle;
        private HealthComponent _playerHealth;

        private Animator _animator;
        private NavMeshAgent _agent;
        private Transform _playerTransform;

        private Action _onEnemyDeath;
        private Action _onPlayerDeath;

        public bool IsDead => _healthLifecycle.IsDead;

        public event Action<IEnemyEntity> Death;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _ragdoll = GetComponent<RagdollEnabler>();
            _healthEnemy = GetComponent<HealthComponent>();

            Vector3 barPosition = transform.position + Vector3.up * _barHeight;
            _healthBarViewInstance = Instantiate(_healthBarViewPrefab, barPosition, Quaternion.identity, transform);
            _healthLifecycle = new HealthLifecycle(_healthEnemy, _healthBarViewInstance, _ragdoll, _agent);

            _onEnemyDeath = () => Death?.Invoke(this);
            _healthLifecycle.Death += _onEnemyDeath;

            _iMovement = new RangeFollower(_attackRange);

            AnimationClip clip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == AttackName);

            float attackDuration = clip?.length ?? 1f;
            _iAttackCooldown = new MeleeAttacker();
            _iAttackCooldown.Initialize(_animator, _attackRange, _attackDamage, attackDuration);
            _agent.enabled = false;
        }

        private void Update()
        {
            if(_playerTransform == null || _healthLifecycle.IsDead)
                return;

            _iAttackCooldown.UpdateCooldown();
            _iMovement.UpdateMovement();

            if(_iAttackCooldown.CanAttack && _iMovement.IsInRange)
                _iAttackCooldown.TryAttack(transform, _playerHealth);
        }

        private void OnDestroy()
        {
            if(_healthLifecycle != null && _onEnemyDeath != null)
                _healthLifecycle.Death -= _onEnemyDeath;

            _healthLifecycle?.Dispose();

            if(_playerHealth != null && _onPlayerDeath != null)
                _playerHealth.Death -= _onPlayerDeath;
        }

        public void Initialize(Transform playerTransform, HealthComponent playerHealth)
        {
            _playerTransform = playerTransform;
            _playerHealth = playerHealth;

            _iMovement.Initialize(_agent, _playerTransform, _animator);
            _onPlayerDeath = OnPlayerDeath;
            _playerHealth.Death += _onPlayerDeath;
        }

        public void Activate()
        {
            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.SetDestination(_playerTransform.position);

            _healthBarViewInstance.Setup(_healthEnemy.MaxHealth);
        }

        public void OnAttackHit()
        {
            if(!IsDead && !_playerHealth.IsDead)
                _playerHealth.TakeDamage(_attackDamage);
        }

        private void OnPlayerDeath()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}