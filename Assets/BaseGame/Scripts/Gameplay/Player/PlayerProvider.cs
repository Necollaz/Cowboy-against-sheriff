using UnityEngine;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Player
{
    public class PlayerProvider : MonoBehaviour, IPlayerProvider
    {
        [SerializeField] private Transform _playerTransform;

        private HealthComponent _playerHealth;

        private void Awake()
        {
            _playerHealth = _playerTransform.GetComponent<HealthComponent>();
        }

        public Transform PlayerTransform => _playerTransform;
        
        public HealthComponent PlayerHealth => _playerHealth;
    }
}