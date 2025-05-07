using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Common.Configs
{
    public class WaypointContainer : MonoBehaviour
    {
        [SerializeField] private WaypointData[] _waypointsData;
        
        public WaypointData[] WaypointsData => _waypointsData;
    }
}