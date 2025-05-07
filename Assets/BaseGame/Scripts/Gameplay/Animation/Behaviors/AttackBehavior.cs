using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Enums;

namespace BaseGame.Scripts.Gameplay.Animation.Behaviors
{
    public class AttackBehavior : State
    {
        private const string AttackName = "Attack";
        
        private readonly float _attackAnimDuration;

        public AttackBehavior(StateMachineContext context) : base(context)
        {
            Animator animator = StateMachineContext.Mover.GetComponent<Animator>();
            AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == AttackName);
            _attackAnimDuration = clip?.length ?? 0.5f;
        }

        public override void Enter()
        {
            StateMachineContext.Agent.isStopped = true;
            StateMachineContext.Animator.PlayIdle();
        }

        public override void HandleInput()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            
            StateMachineContext.Animator.PlayPlayerAttack();
            
            Ray ray = StateMachineContext.Camera.ScreenPointToRay(Input.mousePosition);
            float planeY = StateMachineContext.Mover.MuzzlePoint.position.y;
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, planeY, 0));

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 spawnPos = StateMachineContext.Mover.MuzzlePoint.position;
                Vector3 direction = (hitPoint - spawnPos).normalized;
                
                StateMachineContext.Spawner.Spawn(direction);
            }
        }
        
        public override void LogicUpdate()
        {
            if (StateMachineContext.Mover.CurrentEnemies.All(enemy => enemy.IsDead))
                StateMachineContext.Machine.ChangeState(StateType.Move);
        }

        public override void Exit()
        {
            StateMachineContext.Animator.ResetTrigger(AnimationIDs.AttackPlayer);
        }
    }
}