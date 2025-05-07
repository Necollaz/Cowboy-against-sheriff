using UnityEngine;
using UnityEngine.AI;

namespace BaseGame.Scripts.Gameplay.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyRangeMover : MonoBehaviour
    {
        [SerializeField] private float _attackRange = 1.5f;

        private NavMeshAgent _agent;
        private Animator _animator;
        private Transform _target;
        private RangeFollower _follower;

        public bool IsInRange => _follower.IsInRange;
        public float AttackRange => _attackRange;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            
            _follower = new RangeFollower(_attackRange);
        }

        public void Initialize(Transform player, Animator animatorOverride = null)
        {
            _follower.Initialize(_agent, player, animatorOverride ?? _animator);
        }

        public void Tick()
        {
            _follower.UpdateMovement();
        }
    }
}