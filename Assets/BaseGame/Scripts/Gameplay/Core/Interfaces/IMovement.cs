using UnityEngine;
using UnityEngine.AI;

namespace BaseGame.Scripts.Gameplay.Common.Interfaces
{
    public interface IMovement
    {
        bool IsInRange { get; }
        
        public void Initialize(NavMeshAgent agent, Transform target, Animator animator);
        
        public void UpdateMovement();
        
        public void Stop();
    }
}