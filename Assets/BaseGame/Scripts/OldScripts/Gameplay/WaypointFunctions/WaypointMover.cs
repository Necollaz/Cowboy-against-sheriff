using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Animation;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Animation.StateMachineSystem.StateFunction;
using BaseGame.Scripts.Gameplay.Character.Common;

namespace BaseGame.Scripts.Gameplay.WaypointFunctions
{
    public class WaypointMover : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private BulletPool _bulletPool; 
        [SerializeField] private float _speed = 3f;

        private IStateObject _currentState;
        private ICharacterAnimator _animator;
        private NavMeshAgent _agent;
        private int _currentIndex;
        
        public Transform CurrentWaypoint => _waypoints[_currentIndex];
        public int CurrentIndex => _currentIndex; 
        public float Speed => _speed;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = new PlayerAnimationAttack(GetComponent<Animator>());
        }

        private void Start()
        {
            _currentIndex = 0;
            transform.position = _waypoints[0].position;
            
            _currentState = new IdleState(this, _animator, _agent, _bulletPool);
            _currentState.EnterState();
        }

        private void Update()
        {
            _currentState.HandleInputPlayer();
            _currentState.UpdateState();
        }

        public void ChangeState(IStateObject newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void OnReachWaypoint()
        {
            _currentState.ExitState();
            _currentIndex++;
            
            if (_currentIndex >= _waypoints.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
                return;
            }
            
            _currentState = new IdleState(this, _animator, _agent, _bulletPool);
            _currentState.EnterState();
        }
    }
}