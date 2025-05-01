using BaseGame.Scripts.Gameplay.Character.ShootFunction;
using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Character.Common
{
    public interface IBulletPool
    {
        public Bullet GetBullet(Vector3 position, Quaternion rotation);
        
        public void ReturnBullet(Bullet bullet);
    }
}