using UnityEngine;
using BaseGame.Scripts.Gameplay.Configuration;
using BaseGame.Scripts.Gameplay.Core.Interfaces;
using BaseGame.Scripts.Gameplay.Pooling;

namespace BaseGame.Scripts.Gameplay.Projectile
{
    public class BulletSpawner : IAmmunitionSpawner
    {
        private readonly ObjectPool<Bullet> _pool;
        private readonly Transform _muzzlePoint;
        private readonly BulletConfig _config;

        public BulletSpawner(Bullet prefab, Transform muzzle, BulletConfig config)
        {
            _config = config;
            _muzzlePoint = muzzle;
            _pool = new ObjectPool<Bullet>(prefab, _config.InitialPoolSize, muzzle);
        }

        public void Spawn(Vector3 direction)
        {
            Vector3 spawnPosition = _muzzlePoint.position + direction.normalized * _config.SpawnOffset;
            Bullet bullet = _pool.Get();
            
            bullet.transform.SetPositionAndRotation(spawnPosition, Quaternion.LookRotation(direction));
            bullet.Initialize(_pool, _config, spawnPosition, direction);
        }
    }
}