using System;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Health
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;
        
        private float _health;

        public event Action Death;
        
        private void Awake() => _health = _maxHealth;

        public bool IsDead => _health <= 0;

        public void TakeDamage(float amount)
        {
            if(IsDead)
                return;
            
            _health -= amount;

            if(_health <= 0)
                Death?.Invoke();
        }
    }
}