using System;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Enemy;

namespace BaseGame.Scripts.Gameplay.Configuration
{
    [Serializable]
    public class WaypointData
    {
        [SerializeField] private Transform _waypoint;
        [SerializeField] private EnemyEntity[] _enemies;

        public Transform Waypoint => _waypoint;
        public EnemyEntity[] Enemies => _enemies;
    }
}