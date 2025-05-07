using System;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Ragdoll;

namespace BaseGame.Scripts.Gameplay.Health
{
    public class HealthLifecycle
    {
        private readonly HealthComponent _health;
        private readonly HealthBarView _healthBarView;
        private readonly RagdollEnabler _ragdoll;
        private readonly NavMeshAgent _agent;

        public event Action Death;

        public HealthLifecycle(HealthComponent health, HealthBarView healthBarView, RagdollEnabler ragdoll, NavMeshAgent agent)
        {
            _health = health;
            _healthBarView = healthBarView;
            _ragdoll = ragdoll;
            _agent = agent;

            _health.HealthChanged += _healthBarView.OnHealthChanged;
            _health.Death += OnDeath;
        }

        private void OnDeath()
        {
            Death?.Invoke();
            _ragdoll.ActivateRagdoll();
            _agent.enabled = false;
            _healthBarView.OnDeath();
        }

        public bool IsDead => _health.IsDead;

        public void Dispose()
        {
            _health.HealthChanged -= _healthBarView.OnHealthChanged;
            _health.Death -= OnDeath;
        }
    }
}