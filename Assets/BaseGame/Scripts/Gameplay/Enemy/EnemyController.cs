using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Health;
using BaseGame.Scripts.Gameplay.Ragdoll;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(RagdollActivator))]
    [RequireComponent(typeof(HealthComponent))]
    public class EnemyController : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackDamage = 10f;

        private IPlayerProvider _playerProvider;
        private RagdollActivator _ragdoll;
        private HealthComponent _health;
        private HealthComponent _playerHealth;
        private Transform _playerTransform;
        private Animator _animator;
        private NavMeshAgent _agent;
        private Coroutine _attackRoutine;

        private float _attackCooldownTimer;
        private float _attackDuration;
        private bool _activated;
        private bool _inAttackRange;

        public bool IsDead => _health.IsDead;

        public event Action<IEnemy> Death;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _ragdoll = GetComponent<RagdollActivator>();
            _health = GetComponent<HealthComponent>();

            _agent.enabled = false;

            _health.Death += HandleDeath;

            AnimationClip clip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "AttackEnemy");
            _attackDuration = clip != null ? clip.length : 1f;

            _attackCooldownTimer = 0f;
        }

        private void Update()
        {
            if(!_activated || _health.IsDead)
                return;

            if(_attackCooldownTimer > 0f)
                _attackCooldownTimer -= Time.deltaTime;

            float distance = Vector3.Distance(transform.position, _playerTransform.position);
            bool nowInRange = distance <= _attackRange;

            if(!nowInRange)
            {
                if(_inAttackRange)
                {
                    _inAttackRange = false;
                    _animator.ResetTrigger(AnimationIDs.AttackEnemy);
                }

                if(!_agent.enabled)
                    _agent.enabled = true;

                _agent.isStopped = false;
                _animator.SetBool(AnimationIDs.Run, true);
                _agent.SetDestination(_playerTransform.position);
            }
            else
            {
                if(!_inAttackRange)
                {
                    _inAttackRange = true;
                    _agent.isStopped = true;
                    _animator.SetBool(AnimationIDs.Run, false);
                }

                if(_attackCooldownTimer <= 0f)
                {
                    _animator.SetTrigger(AnimationIDs.AttackEnemy);
                    _attackCooldownTimer = _attackDuration;
                }
            }
        }

        public void Initialize(Transform playerTransform, HealthComponent playerHealth)
        {
            _playerTransform = playerTransform;
            _playerHealth = playerHealth;

            _playerHealth.Death += OnPlayerDeath;
        }
        
        public void Activate()
        {
            _activated = true;
            _agent.enabled = true;
            _agent.isStopped = false;

            _agent.SetDestination(_playerTransform.position);
        }

        public void OnAttackHit()
        {
            if(!_health.IsDead && !_playerHealth.IsDead)
                _playerHealth.TakeDamage(_attackDamage);
        }

        private void HandleDeath()
        {
            Death?.Invoke(this);

            _ragdoll.ActivateRagdoll();
            _agent.enabled = false;
        }

        private void OnPlayerDeath()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
