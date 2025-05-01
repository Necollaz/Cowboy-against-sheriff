using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.WaypointFunctions;

namespace BaseGame.Scripts.Gameplay.Animation.StateMachineSystem.StateFunction
{
    public class MoveState : IStateObject
    {
        private readonly WaypointMover _mover;
        private readonly ICharacterAnimator _animator;
        private readonly NavMeshAgent _agent;

        public MoveState(WaypointMover mover, ICharacterAnimator animator, NavMeshAgent agent)
        {
            _mover = mover;
            _animator = animator;
            _agent = agent;
        }

        public void EnterState()
        {
            _agent.speed = _mover.Speed;
            _agent.isStopped = false;
            _agent.SetDestination(_mover.CurrentWaypoint.position);
            _animator.PlayRun();
        }

        public void UpdateState()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                _mover.OnReachWaypoint();
            }
        }

        public void HandleInputPlayer()
        {
        }

        public void ExitState()
        {
        }
    }
}