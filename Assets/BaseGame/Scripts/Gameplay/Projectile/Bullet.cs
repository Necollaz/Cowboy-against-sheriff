using System;
using System.Collections;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Configuration;
using BaseGame.Scripts.Gameplay.Core.Interfaces;

namespace BaseGame.Scripts.Gameplay.Projectile
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Coroutine _lifeCoroutine;
        
        private float _lifeTime;
        private float _damage;
        
        public event Action<Bullet> Finished;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(BulletConfig config, Vector3 direction)
        {
            _damage = config.Damage;
            _lifeTime = config.LifeTime;
            
            gameObject.SetActive(true);

            _rigidbody.velocity = direction.normalized * config.Speed;
            _lifeCoroutine = StartCoroutine(LifeTimer());
        }

        private IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(_lifeTime);
            
            Finish();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable target))
                target.TakeDamage(_damage);
            
            Finish();
        }

        private void Finish()
        {
            if (_lifeCoroutine != null)
            {
                StopCoroutine(_lifeCoroutine); _lifeCoroutine = null;
            }
            
            Finished?.Invoke(this);
            
            gameObject.SetActive(false);
        }
    }
}