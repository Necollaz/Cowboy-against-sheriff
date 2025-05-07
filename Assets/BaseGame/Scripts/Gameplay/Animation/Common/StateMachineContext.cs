using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Animation.AnimatorComponents;
using BaseGame.Scripts.Gameplay.Projectile;
using BaseGame.Scripts.Gameplay.Waypoint;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class StateMachineContext
    {
        public PathNavigator Mover { get; }
        public NavMeshAgent Agent { get; }
        public CharacterAnimator Animator { get; }
        public BulletSpawner Spawner { get; }
        public StateMachine Machine { get; set; }
        public Camera Camera { get; }

        public StateMachineContext(PathNavigator mover, NavMeshAgent agent, CharacterAnimator animator, BulletSpawner spawner, Camera camera)
        {
            Mover = mover;
            Agent = agent;
            Animator = animator;
            Spawner = spawner;
            Camera = camera;
        }
    }
}