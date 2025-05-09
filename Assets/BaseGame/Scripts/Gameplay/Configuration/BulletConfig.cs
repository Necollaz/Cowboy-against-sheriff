using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Configuration
{
    [CreateAssetMenu(menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private float _damage = 25f;
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private float _spawnOffset = 0.1f;
        [SerializeField] private int _poolSize = 20;
        
        public float Damage => _damage;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;
        public float SpawnOffset => _spawnOffset;
        public int InitialPoolSize => _poolSize;
    }
}