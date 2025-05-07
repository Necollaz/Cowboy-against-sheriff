using System.Collections;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.PoolSystem;

namespace BaseGame.Scripts.Gameplay.ShootComponents
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 25f;

        private ObjectPool<Bullet> _pool;
        private Rigidbody _rigidbody;
        private float _lifeTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(ObjectPool<Bullet> pool, Vector3 direction, float speed, float lifeTime)
        {
            _pool = pool;
            _lifeTime = lifeTime;

            transform.gameObject.SetActive(true);
            
            _rigidbody.velocity = direction.normalized * speed;
            
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
                target.TakeDamage(_damage);
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            StopAllCoroutines();
            
            _pool.Return(this);
        }
    }
}