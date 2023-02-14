using CounterSystem;
using EmreBeratKR.ServiceLocator;
using UnityEngine;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController body;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private PlayerInteractor interactor;
        [SerializeField] private KitchenObjectSlot slot;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotationSpeed = 25f;


        public bool IsFull => slot.IsFull;
        

        private Counter m_CurrentCounter;
        

        private void OnEnable()
        {
            ServiceLocator.Get<GameInput>().OnInteract += OnGameInputInteract;
        }

        private void OnDisable()
        {
            ServiceLocator.Get<GameInput>().OnInteract -= OnGameInputInteract;
        }

        private void Update()
        {
            var direction = GetDirectionNormalized();
        
            HandleMovement(direction);
            HandleInteraction();
        }


        public bool TryPutKitchenObject(KitchenObject kitchenObject)
        {
            return slot.TryPut(kitchenObject);
        }
        
        public bool TryGetKitchenObject(out KitchenObject kitchenObject)
        {
            return slot.TryGet(out kitchenObject);
        }

        public bool TryRemoveKitchenObject(out KitchenObject kitchenObject)
        {
            return slot.TryRemove(out kitchenObject);
        }
        

        private void OnGameInputInteract()
        {
            if (!m_CurrentCounter) return;
            
            m_CurrentCounter.Interact(this);
        }


        private void HandleMovement(Vector3 direction)
        {
            UpdatePosition(direction);
            UpdateRotation(direction);
            UpdateAnimator(direction);
        }

        private void HandleInteraction()
        {
            if (interactor.TryGetInteraction(out Counter currentCounter))
            {
                if (m_CurrentCounter == currentCounter) return;
                
                if (m_CurrentCounter) m_CurrentCounter.Deselect();
                
                currentCounter.Select();
                m_CurrentCounter = currentCounter;
                return;
            }
            
            if (m_CurrentCounter) m_CurrentCounter.Deselect();
            
            m_CurrentCounter = null;
        }

        private void UpdatePosition(Vector3 direction)
        {
            var motion = direction * (moveSpeed * Time.deltaTime);
            body.Move(motion);
        }

        private void UpdateRotation(Vector3 direction)
        {
            var forward = Vector3.Slerp(body.transform.forward, direction, Time.deltaTime * rotationSpeed);
            body.transform.forward = forward;
        }

        private void UpdateAnimator(Vector3 direction)
        {
            animator.IsWalking = direction != Vector3.zero;
        }

        private static Vector3 GetDirectionNormalized()
        {
            return ServiceLocator
                .Get<GameInput>()
                .GetDirectionNormalized();
        }
    }
}
