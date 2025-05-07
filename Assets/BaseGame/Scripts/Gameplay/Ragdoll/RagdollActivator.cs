using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Ragdoll
{
    [RequireComponent(typeof(Animator))]
    public class RagdollActivator : MonoBehaviour, IRagdollActivator
    {
        [SerializeField] private Collider[] _ragdollColliders;
        [SerializeField] private Rigidbody[] _ragdollBodies;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            foreach (Collider enemyCollider in _ragdollColliders)
                enemyCollider.enabled = false;
            
            foreach (Rigidbody enemyRigidbody in _ragdollBodies)
                enemyRigidbody.isKinematic = true;
        }

        public void ActivateRagdoll()
        {
            _animator.enabled = false;
            
            foreach (Collider enemyCollider in _ragdollColliders)
                enemyCollider.enabled = true;
            
            foreach (Rigidbody enemyRigidbody in _ragdollBodies)
                enemyRigidbody.isKinematic = false;
        }
    }
}