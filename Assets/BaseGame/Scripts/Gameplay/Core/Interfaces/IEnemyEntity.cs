using System;

namespace BaseGame.Scripts.Gameplay.Core.Interfaces
{
    public interface IEnemyEntity
    {
        public bool IsDead { get; }

        public event Action<IEnemyEntity> Death;

        public void Activate();
    }
}