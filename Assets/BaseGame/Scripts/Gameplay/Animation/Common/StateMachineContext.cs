using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.WaypointSystem;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class StateMachineContext
    {
        public WaypointMover Mover { get; }
        public NavMeshAgent Agent { get; }
        public ICharacterAnimator Animator { get; }
        public IBulletSpawner Spawner { get; }
        public StateMachine Machine { get; set; }
        public Camera Camera { get; }

        public StateMachineContext(WaypointMover mover, NavMeshAgent agent, ICharacterAnimator animator, IBulletSpawner spawner, Camera camera)
        {
            Mover = mover;
            Agent = agent;
            Animator = animator;
            Spawner = spawner;
            Camera = camera;
        }
    }
}