using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;

namespace BaseGame.Scripts.Gameplay.Animation.Behaviors
{
    public class MoveBehavior : State
    {
        public MoveBehavior(StateMachineContext context) : base(context) { }
        
        public override void Enter()
        {
            StateMachineContext.Agent.speed = StateMachineContext.Mover.Speed;
            StateMachineContext.Agent.isStopped = false;
            
            Vector3 target = StateMachineContext.Mover.HasNextWaypoint ? StateMachineContext.Mover.NextPosition
                : StateMachineContext.Mover.CurrentData.Waypoint.position;
            
            StateMachineContext.Agent.SetDestination(target);
            StateMachineContext.Animator.PlayRun();
        }
        
        public override void LogicUpdate()
        {
            if (!StateMachineContext.Agent.pathPending && StateMachineContext.Agent.remainingDistance <= StateMachineContext.Agent.stoppingDistance)
                StateMachineContext.Mover.OnReachWaypoint();
        }
    }
}