using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Enums;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Animation.States
{
    public class AttackState : IState
    {
        private const string AttackName = "Attack";
        
        private readonly StateMachineContext _stateMachineContext;
        private readonly IBulletSpawner _spawner;
        private readonly Camera _camera;
        
        private readonly float _attackAnimDuration;

        public AttackState(StateMachineContext stateMachineContext)
        {
            _stateMachineContext = stateMachineContext;
            _spawner = _stateMachineContext.Spawner;
            _camera = _stateMachineContext.Camera;
            
            Animator animator = _stateMachineContext.Mover.GetComponent<Animator>();

            AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == AttackName);
            _attackAnimDuration = clip != null ? clip.length : 0.5f;
        }

        public void Enter()
        {
            _stateMachineContext.Agent.isStopped = true;
            _stateMachineContext.Animator.PlayIdle();
        }

        public void HandleInput()
        {
            if(!Input.GetMouseButtonDown(0)) return;
            
            _stateMachineContext.Animator.PlayPlayerAttack();
            
            if (_camera == null)
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 direction = Physics.Raycast(ray, out var hit, 100f) ? hit.point - _stateMachineContext.Mover.transform.position : ray.direction;

            _stateMachineContext.Spawner.Spawn(direction);
        }

        public void LogicUpdate()
        {
            if (_stateMachineContext.Mover.CurrentEnemies.All(e => e.IsDead))
                _stateMachineContext.Machine.ChangeState(StateType.Move);
        }

        public void Exit()
        {
            _stateMachineContext.Animator.ResetTrigger(AnimationIDs.AttackPlayer);
        }
    }
}