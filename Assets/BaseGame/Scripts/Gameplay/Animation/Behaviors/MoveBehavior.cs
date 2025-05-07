using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Animation.Behaviors
{
    public class MoveBehavior : IState
    {
        private readonly StateMachineContext _stateMachineContext;
        
        public MoveBehavior(StateMachineContext stateMachineContext) => _stateMachineContext = stateMachineContext;
        
        public void Enter()
        {
            _stateMachineContext.Agent.speed = _stateMachineContext.Mover.Speed;
            _stateMachineContext.Agent.isStopped = false;
            
            Vector3 target = _stateMachineContext.Mover.HasNextWaypoint ? _stateMachineContext.Mover.NextPosition : _stateMachineContext.Mover.CurrentData.Waypoint.position;
            
            _stateMachineContext.Agent.SetDestination(target);
            _stateMachineContext.Animator.PlayRun();
        }

        public void HandleInput()
        {
        }
        
        public void LogicUpdate()
        {
            if (!_stateMachineContext.Agent.pathPending && _stateMachineContext.Agent.remainingDistance <= _stateMachineContext.Agent.stoppingDistance)
                _stateMachineContext.Mover.OnReachWaypoint();
        }

        public void Exit()
        {
        }
    }
}