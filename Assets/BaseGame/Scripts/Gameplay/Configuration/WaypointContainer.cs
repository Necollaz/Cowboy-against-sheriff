using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Configuration
{
    public class WaypointContainer : MonoBehaviour
    {
        [SerializeField] private WaypointData[] _waypointsData;
        
        public WaypointData[] WaypointsData => _waypointsData;
    }
}