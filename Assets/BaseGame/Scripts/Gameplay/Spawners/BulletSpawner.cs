using UnityEngine;
using BaseGame.Scripts.Gameplay.Common.Configs;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.PoolSystem;
using BaseGame.Scripts.Gameplay.ShootComponents;

namespace BaseGame.Scripts.Gameplay.Spawners
{
    public class BulletSpawner : IBulletSpawner
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
            Vector3 spawnPos = _muzzlePoint.position + direction.normalized * _config.SpawnOffset;
            Bullet bullet = _pool.Get();
            
            bullet.transform.SetPositionAndRotation(spawnPos, Quaternion.LookRotation(direction));
            bullet.Initialize(_pool, direction, _config.Speed, _config.LifeTime);
        }
    }
}