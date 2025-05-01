using System.Collections.Generic;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Character.ShootFunction;

namespace BaseGame.Scripts.Gameplay.Character.Common
{
    public class BulletPool : MonoBehaviour, IBulletPool
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _initialSize = 20;

        private Queue<Bullet> _pool;

        private void Awake()
        {
            _pool = new Queue<Bullet>();

            for(int i = 0; i < _initialSize; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, transform);
                bullet.gameObject.SetActive(false);
                bullet.Initialize(this, Vector3.zero, 0f);
                _pool.Enqueue(bullet);
            }
        }

        public Bullet GetBullet(Vector3 pos, Quaternion rot)
        {
            Bullet bullet;

            if(_pool.Count > 0)
                bullet = _pool.Dequeue();
            else
                bullet = Instantiate(_bulletPrefab, transform);

            bullet.transform.SetPositionAndRotation(pos, rot);
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }
}