using System;
using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Health;
using BaseGame.Scripts.Gameplay.Ragdoll;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(RagdollEnabler))]
    public class EnemyHealthPresenter : MonoBehaviour
    {
        [SerializeField] private HealthBarView _barPrefab;
        [SerializeField] private float _barHeight = 2f;

        private HealthBarView _barInstance;
        private RagdollEnabler _ragdoll;
        private NavMeshAgent _agent;
        private HealthComponent _health;

        public event Action Death;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _ragdoll = GetComponent<RagdollEnabler>();
            _agent = GetComponent<NavMeshAgent>();

            Vector3 position = transform.position + Vector3.up * _barHeight;
            _barInstance = Instantiate(_barPrefab, position, Quaternion.identity, transform);
            _barInstance.Setup(_health.MaxHealth);

            _health.HealthChanged += _barInstance.OnHealthChanged;
            _health.Death += OnDeathInternal;
        }
        
        private void OnDestroy()
        {
            _health.HealthChanged -= _barInstance.OnHealthChanged;
            _health.Death -= OnDeathInternal;
        }

        private void OnDeathInternal()
        {
            Death?.Invoke();

            _ragdoll.Activate();
            _agent.enabled = false;
            _barInstance.OnDeath();
        }
    }
}