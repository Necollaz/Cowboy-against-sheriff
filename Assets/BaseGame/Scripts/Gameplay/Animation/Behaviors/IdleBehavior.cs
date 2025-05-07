using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Enums;

namespace BaseGame.Scripts.Gameplay.Animation.Behaviors
{
    public class IdleBehavior : State
    {
        public IdleBehavior(StateMachineContext context) : base(context) { }

        public override void Enter()
        {
            StateMachineContext.Agent.isStopped = true;
            StateMachineContext.Animator.PlayIdle();
        }
        
        public override void HandleInput()
        {
            if (StateMachineContext.Mover.CurrentIndex == 0)
            {
                if (Input.anyKeyDown)
                    StateMachineContext.Machine.ChangeState(StateType.Move);
            }
            else if (StateMachineContext.Mover.HasNextWaypoint && Input.GetMouseButtonDown(0))
            {
                StateMachineContext.Machine.ChangeState(StateType.Attack);
            }
        }
    }
}