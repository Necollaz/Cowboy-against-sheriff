using System;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Enemy;

namespace BaseGame.Scripts.Gameplay.Common.Configs
{
    [Serializable]
    public class WaypointData
    {
        [SerializeField] private Transform _waypoint;
        [SerializeField] private EnemyController[] _enemies;

        public Transform Waypoint => _waypoint;
        public EnemyController[] Enemies => _enemies;
    }
}