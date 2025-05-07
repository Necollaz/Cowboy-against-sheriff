using UnityEngine;
using UnityEngine.AI;
using BaseGame.Scripts.Gameplay.Common.Interfaces;
using BaseGame.Scripts.Gameplay.Core.Interfaces;
using BaseGame.Scripts.Gameplay.Waypoint;

namespace BaseGame.Scripts.Gameplay.Animation.Common
{
    public class StateMachineContext
    {
        public PathNavigator Mover { get; }
        public NavMeshAgent Agent { get; }
        public ICharacterAnimator Animator { get; }
        public IAmmunitionSpawner Spawner { get; }
        public StateMachine Machine { get; set; }
        public Camera Camera { get; }

        public StateMachineContext(PathNavigator mover, NavMeshAgent agent, ICharacterAnimator animator, IAmmunitionSpawner spawner, Camera camera)
        {
            Mover = mover;
            Agent = agent;
            Animator = animator;
            Spawner = spawner;
            Camera = camera;
        }
    }
}