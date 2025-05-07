using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Common.Interfaces;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    public class RangeFollower : IMovement
    {
        private readonly float _attackRange;
        
        private NavMeshAgent _agent;
        private Transform _target;
        private Animator _animator;

        private bool _isInRange;
        private bool _wasMoving;

        public bool IsInRange => _isInRange;

        public RangeFollower(float attackRange)
        {
            _attackRange = attackRange;
        }

        public void Initialize(NavMeshAgent agent, Transform target, Animator animator)
        {
            _agent = agent;
            _target = target;
            _animator = animator;
            _agent.enabled = true;
            _wasMoving = false;
        }

        public void UpdateMovement()
        {
            float dist = Vector3.Distance(_agent.transform.position, _target.position);
            _isInRange = dist <= _attackRange;

            bool isMoving = !_isInRange;
            
            if(isMoving != _wasMoving)
            {
                _animator.SetBool(AnimationIDs.Run, isMoving);
                _wasMoving = isMoving;
            }

            if(isMoving)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        public void Stop()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
            _animator.SetBool(AnimationIDs.Run, false);
            _wasMoving = false;
        }
    }
}