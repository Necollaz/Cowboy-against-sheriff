using BaseGame.Scripts.Gameplay.Character.Common;
using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Character.ShootFunction
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 20f;

        private IBulletPool _pool;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(IBulletPool pool, Vector3 direction, float speedMultiplier)
        {
            _pool = pool;
            _rigidbody.velocity = direction.normalized * (_speed * speedMultiplier);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _pool?.ReturnBullet(this);
        }
    }
}
