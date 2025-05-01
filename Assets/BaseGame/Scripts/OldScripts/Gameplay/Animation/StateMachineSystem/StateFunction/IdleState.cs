using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Character.Common;
using BaseGame.Scripts.Gameplay.Character.ShootFunction;
using BaseGame.Scripts.Gameplay.WaypointFunctions;

namespace BaseGame.Scripts.Gameplay.Animation.StateMachineSystem.StateFunction
{
    public class IdleState : IStateObject
    {
        private readonly WaypointMover _mover;
        private readonly ICharacterAnimator _animator;
        private readonly NavMeshAgent _agent;
        private readonly IBulletPool _bulletPool;
        
        public IdleState(WaypointMover mover, ICharacterAnimator animator, NavMeshAgent agent, IBulletPool bulletPool)
        {
            _mover = mover;
            _animator = animator;
            _agent = agent;
            _bulletPool = bulletPool;
        }

        public void EnterState()
        {
            _agent.isStopped = true;
            _animator.PlayIdle();
        }

        public void UpdateState()
        {
        }

        public void HandleInputPlayer()
        {
            if (_mover.CurrentIndex == 0)
            {
                if (Input.anyKeyDown)
                {
                    _mover.ChangeState(new MoveState(_mover, _animator, _agent));
                }
                
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _mover.ChangeState(new AttackState(_mover, _animator, _agent, _bulletPool));
            }
        }

        public void ExitState() { }
    }
}