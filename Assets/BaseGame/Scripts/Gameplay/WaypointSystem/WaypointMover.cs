using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using BaseGame.Scripts.Gameplay.Animation;
using BaseGame.Scripts.Gameplay.Animation.Common;
using BaseGame.Scripts.Gameplay.Animation.States;
using BaseGame.Scripts.Gameplay.Common.Configs;
using BaseGame.Scripts.Gameplay.Common.Enums;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Enemy;
using BaseGame.Scripts.Gameplay.Health;
using BaseGame.Scripts.Gameplay.ShootComponents;
using BaseGame.Scripts.Gameplay.Spawners;

namespace BaseGame.Scripts.Gameplay.WaypointSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(HealthComponent))]
    public class WaypointMover : MonoBehaviour
    {
        [Header("Configuration")] [SerializeField]
        private WaypointContainer _container;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _muzzlePoint;

        private NavMeshAgent _agent;
        private StateMachine _machine;
        private StateMachineContext _stateMachineContext;
        private BulletSpawner _spawner;
        private HealthComponent _playerHealth;
        private Camera _camera;

        private int _currentIndex;

        public WaypointData CurrentData => _container.WaypointsData[_currentIndex];
        public Vector3 NextPosition => _container.WaypointsData[_currentIndex + 1].Waypoint.position;
        public List<IEnemy> CurrentEnemies { get; } = new List<IEnemy>();
        public int CurrentIndex => _currentIndex;
        public float Speed => _agent.speed;
        public bool HasNextWaypoint => _currentIndex + 1 < _container.WaypointsData.Length;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerHealth = GetComponent<HealthComponent>();
            _camera = Camera.main;

            _spawner = new BulletSpawner(_bulletPrefab, _muzzlePoint, _bulletConfig);
            _stateMachineContext = new StateMachineContext(this, _agent, new CharacterAnimator(GetComponent<Animator>()), _spawner, _camera);

            Dictionary<StateType, IState> states = new Dictionary<StateType, IState> { { StateType.Idle, new IdleState(_stateMachineContext) }, { StateType.Move, new MoveState(_stateMachineContext) }, { StateType.Attack, new AttackState(_stateMachineContext) } };

            _machine = new StateMachine(states);
            _stateMachineContext.Machine = _machine;
        }

        private void Start()
        {
            _currentIndex = 0;
            transform.position = _container.WaypointsData[0].Waypoint.position;
            _machine.ChangeState(StateType.Idle);
        }

        private void Update()
        {
            _machine.Update();
        }

        public void OnReachWaypoint()
        {
            _currentIndex++;

            if(_currentIndex >= _container.WaypointsData.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                return;
            }

            CurrentEnemies.Clear();

            foreach(EnemyController enemy in _container.WaypointsData[_currentIndex].Enemies)
            {
                CurrentEnemies.Add(enemy);
                enemy.Initialize(transform, _playerHealth);
                enemy.Activate();
            }

            _machine.ChangeState(StateType.Idle);
        }
    }
}
