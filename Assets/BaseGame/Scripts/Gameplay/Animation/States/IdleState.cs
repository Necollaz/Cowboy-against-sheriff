using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Enums;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Animation.States
{
    public class IdleState : IState
    {
        private readonly StateMachineContext _stateMachineContext;
        
        public IdleState(StateMachineContext stateMachineContext) => _stateMachineContext = stateMachineContext;

        public void Enter()
        {
            _stateMachineContext.Agent.isStopped = true; _stateMachineContext.Animator.PlayIdle();
        }
        
        public void HandleInput()
        {
            if (_stateMachineContext.Mover.CurrentIndex == 0)
            {
                if (Input.anyKeyDown)
                    _stateMachineContext.Machine.ChangeState(StateType.Move);
            }
            else if (_stateMachineContext.Mover.HasNextWaypoint && Input.GetMouseButtonDown(0))
            {
                _stateMachineContext.Machine.ChangeState(StateType.Attack);
            }
        }

        public void LogicUpdate()
        {
        }

        public void Exit()
        {
        }
    }
}