using UnityEngine;
using BaseGame.Scripts.Gameplay.Projectile;

namespace BaseGame.Scripts.Gameplay.Pooling
{
    public class BulletPool : ObjectPool<Bullet>
    {
        public BulletPool(Bullet prefab, int initialSize, Transform parent) : base(prefab, initialSize, parent)
        {
            foreach (Bullet bullet in Pool)
                bullet.Finished += Return;
        }

        public new Bullet Get()
        {
            Bullet bullet = base.Get();
            
            bullet.Finished += Return;
            
            return bullet;
        }

        private new void Return(Bullet bullet)
        {
            bullet.Finished -= Return;
            
            base.Return(bullet);
        }
    }
}