using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Character.Common;
using BaseGame.Scripts.Gameplay.Character.ShootFunction;
using BaseGame.Scripts.Gameplay.WaypointFunctions;

namespace BaseGame.Scripts.Gameplay.Animation.StateMachineSystem.StateFunction
{
    public class AttackState : IStateObject
    {
        private readonly WaypointMover _mover;
        private readonly ICharacterAnimator _animator;
        private readonly NavMeshAgent _agent;
        private readonly IBulletPool _bulletPool;

        private int _shotsFired;
        private const int ShotsPerPoint = 5;

        public AttackState(WaypointMover mover, ICharacterAnimator animator, NavMeshAgent agent, IBulletPool bulletPool)
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
            _shotsFired = 0;
        }

        public void UpdateState() { }

        public void HandleInputPlayer()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            
            _animator.PlayAttack();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = Physics.Raycast(ray, out var hit, 100f) ? hit.point - _mover.transform.position : ray.direction;
            Vector3 spawnPos = _mover.transform.position + dir.normalized * 1.2f + Vector3.up * 1.0f;
            var bullet = _bulletPool.GetBullet(spawnPos, Quaternion.LookRotation(dir));
            bullet.Initialize(_bulletPool, dir, 1f);
            
            _shotsFired++;
            
            if (_shotsFired >= ShotsPerPoint)
            {
                _mover.ChangeState(new MoveState(_mover, _animator, _agent));
            }
        }

        public void ExitState() { }
    }
}