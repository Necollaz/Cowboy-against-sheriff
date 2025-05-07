using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Animation.AnimatorComponents;
using BaseGame.Scripts.Gameplay.Animation.Behaviors;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Configuration;
using BaseGame.Scripts.Gameplay.Enemy;
using BaseGame.Scripts.Gameplay.Enums;
using BaseGame.Scripts.Gameplay.Health;
using BaseGame.Scripts.Gameplay.Projectile;

namespace BaseGame.Scripts.Gameplay.Waypoint
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PathNavigator : MonoBehaviour
    {
        [SerializeField] private WaypointContainer _container;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _muzzlePoint;

        private NavMeshAgent _agent;
        private StateMachine _machine;
        private StateMachineContext _context;
        private BulletSpawner _spawner;
        private HealthComponent _playerHealth;
        private Camera _camera;

        private int _currentIndex;

        public WaypointData CurrentData => _container.WaypointsData[_currentIndex];
        public Vector3 NextPosition => _container.WaypointsData[_currentIndex + 1].Waypoint.position;
        public Transform MuzzlePoint => _muzzlePoint;
        public List<EnemyEntity> CurrentEnemies { get; } = new List<EnemyEntity>();
        public bool HasNextWaypoint => _currentIndex + 1 < _container.WaypointsData.Length;
        public float Speed => _agent.speed;
        public int CurrentIndex => _currentIndex;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerHealth = GetComponent<HealthComponent>();
            
            _camera = Camera.main;

            _spawner = new BulletSpawner(_bulletPrefab, _muzzlePoint, _bulletConfig);
            _context = new StateMachineContext(this, _agent, new CharacterAnimator(GetComponent<Animator>()), _spawner, _camera);

            Dictionary<StateType, State> states = new Dictionary<StateType, State>
            {
                { StateType.Idle, new IdleBehavior(_context) },
                { StateType.Move, new MoveBehavior(_context) },
                { StateType.Attack, new AttackBehavior(_context) }
            };
            
            _machine = new StateMachine(states);
            _context.Machine = _machine;
        }

        private void Start()
        {
            _currentIndex = 0;
            transform.position = _container.WaypointsData[0].Waypoint.position;
            
            _machine.ChangeState(StateType.Idle);
        }

        private void Update()
        {
            _machine.UpdateState();
        }

        public void OnReachWaypoint()
        {
            _currentIndex++;

            if (_currentIndex >= _container.WaypointsData.Length - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                return;
            }

            CurrentEnemies.Clear();

            foreach (EnemyEntity enemy in _container.WaypointsData[_currentIndex].Enemies)
            {
                CurrentEnemies.Add(enemy);
                enemy.Initialize(transform, _playerHealth);
                enemy.Activate();
            }

            _machine.ChangeState(StateType.Idle);
        }
    }
}