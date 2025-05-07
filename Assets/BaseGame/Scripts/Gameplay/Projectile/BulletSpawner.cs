using UnityEngine;
using BaseGame.Scripts.Gameplay.Configuration;
using BaseGame.Scripts.Gameplay.Pooling;

namespace BaseGame.Scripts.Gameplay.Projectile
{
    public class BulletSpawner
    {
        private readonly BulletPool _pool;
        private readonly Transform _muzzlePoint;
        private readonly BulletConfig _config;

        public BulletSpawner(Bullet prefab, Transform muzzle, BulletConfig config)
        {
            _pool = new BulletPool(prefab, config.InitialPoolSize, muzzle);
            _muzzlePoint = muzzle;
            _config = config;
        }

        public void Spawn(Vector3 direction)
        {
            Vector3 spawnPos = _muzzlePoint.position + direction.normalized * _config.SpawnOffset;
            Bullet bullet = _pool.Get();
            
            bullet.transform.SetPositionAndRotation(spawnPos, Quaternion.LookRotation(direction));
            bullet.Initialize(_config, direction);
        }
    }
}