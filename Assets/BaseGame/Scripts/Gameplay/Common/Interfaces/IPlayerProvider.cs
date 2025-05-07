using UnityEngine;
using BaseGame.Scripts.Gameplay.Health;

namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface IPlayerProvider
    {
        public Transform PlayerTransform { get; }
        public HealthComponent PlayerHealth { get; }
    }
}