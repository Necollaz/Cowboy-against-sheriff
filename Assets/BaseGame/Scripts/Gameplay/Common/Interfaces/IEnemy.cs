using System;

namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface IEnemy
    {
        public bool IsDead { get; }

        public event Action<IEnemy> Death;

        public void Activate();
    }
}