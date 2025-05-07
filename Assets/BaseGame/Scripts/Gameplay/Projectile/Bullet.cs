using System.Collections;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Configuration;
using BaseGame.Scripts.Gameplay.Pooling;

namespace BaseGame.Scripts.Gameplay.Projectile
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private BulletConfig _config;
        private ObjectPool<Bullet> _pool;
        private Rigidbody _rigidbody;
        private float _lifeTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(ObjectPool<Bullet> pool, BulletConfig config, Vector3 spawnPosition, Vector3 direction)
        {
            _pool = pool;
            _config = config;
            _lifeTime = _config.LifeTime;
            transform.position = spawnPosition;

            transform.gameObject.SetActive(true);
            
            _rigidbody.velocity = direction.normalized * config.Speed;
            
            StartCoroutine(LifeTimer());
        }

        private IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(_lifeTime);
            
            ReturnToPool();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable target))
                target.TakeDamage(_config.Damage);
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            StopAllCoroutines();
            
            _pool.Return(this);
        }
    }
}