using System;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Core.Interfaces;

namespace BaseGame.Scripts.Gameplay.Health
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;
        
        private float _health;
        
        public float MaxHealth => _maxHealth;
        public bool IsDead => _health <= 0f;
        
        public event Action Death;
        public event Action<float, float> HealthChanged;
        
        private void Awake()
        {
            _health = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (IsDead)
                return;

            _health = Mathf.Max(0f, _health - amount);
            
            HealthChanged?.Invoke(_health, _maxHealth);

            if (_health <= 0f)
                Death?.Invoke();
        }
    }
}