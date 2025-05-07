using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Core.Interfaces;
using BaseGame.Scripts.Gameplay.Enums;

namespace BaseGame.Scripts.Gameplay.Animation.Behaviors
{
    public class AttackBehavior : IState
    {
        private const string AttackName = "Attack";
        
        private readonly StateMachineContext _stateMachineContext;
        private readonly IAmmunitionSpawner _spawner;
        private readonly Camera _camera;
        
        private readonly float _attackAnimDuration;

        public AttackBehavior(StateMachineContext stateMachineContext)
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
            
            Ray ray = _stateMachineContext.Camera.ScreenPointToRay(Input.mousePosition);
            float planeY = _stateMachineContext.Mover.MuzzlePoint.position.y;
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, planeY, 0));

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 spawnPos = _stateMachineContext.Mover.MuzzlePoint.position;
                Vector3 direction = (hitPoint - spawnPos).normalized;
                
                _stateMachineContext.Spawner.Spawn(direction);
            }
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