using System;
using CounterSystem;
using EmreBeratKR.ServiceLocator;
using KitchenObjectSystem;
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


        public event Action OnStartMoving;
        public event Action OnStopMoving;
        public event Action<PickupOrDropKitchenObjectArgs> OnPickupKitchenObject; 
        public event Action<PickupOrDropKitchenObjectArgs> OnDropKitchenObject; 
        public struct PickupOrDropKitchenObjectArgs
        {
            public KitchenObject kitchenObject;
        }
        

        public bool IsFull => slot.IsFull;
        public bool IsEmpty => slot.IsEmpty;
        

        private Counter m_CurrentCounter;
        private bool m_IsMoving;
        

        private void OnEnable()
        {
            ServiceLocator.Get<GameInput>().OnInteract += OnGameInputInteract;
            ServiceLocator.Get<GameInput>().OnInteractAlternate += OnGameInputInteractAlternate;
        }

        private void OnDisable()
        {
            ServiceLocator.Get<GameInput>().OnInteract -= OnGameInputInteract;
            ServiceLocator.Get<GameInput>().OnInteractAlternate -= OnGameInputInteractAlternate;
        }

        private void Update()
        {
            var direction = GetDirectionNormalized();
        
            HandleMovement(direction);
            HandleInteraction();
        }


        public void ClearKitchenObject()
        {
            if (slot.TryGet(out KitchenObject kitchenObject))
            {
                OnDropKitchenObject?.Invoke(new PickupOrDropKitchenObjectArgs
                {
                    kitchenObject = kitchenObject
                });
            }
            
            slot.Clear();
        }
        
        public bool TryPutKitchenObject(KitchenObject kitchenObject)
        {
            var put = slot.TryPut(kitchenObject);

            if (!put) return false;
            
            OnPickupKitchenObject?.Invoke(new PickupOrDropKitchenObjectArgs
            {
                kitchenObject = kitchenObject
            });

            return true;
        }
        
        public bool TryGetKitchenObject<T>(out T kitchenObject)
            where T : KitchenObject
        {
            return slot.TryGet(out kitchenObject);
        }

        public bool TryRemoveKitchenObject<T>(out T kitchenObject)
            where T : KitchenObject
        {
            var removed = slot.TryRemove(out kitchenObject);

            if (!removed) return false;
            
            OnDropKitchenObject?.Invoke(new PickupOrDropKitchenObjectArgs
            {
                kitchenObject = kitchenObject
            });

            return true;
        }

        public bool ContainsKitchenObject(KitchenObjectSO kitchenObject)
        {
            return slot.Contains(kitchenObject);
        }
        

        private void OnGameInputInteract()
        {
            if (!m_CurrentCounter) return;
            
            m_CurrentCounter.Interact(this);
        }

        private void OnGameInputInteractAlternate()
        {
            if (!m_CurrentCounter) return;
            
            m_CurrentCounter.InteractAlternate(this);
        }


        private void HandleMovement(Vector3 direction)
        {
            UpdatePosition(direction);
            UpdateRotation(direction);
            UpdateMovementState();
            UpdateAnimator();
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
        
        private void UpdateMovementState()
        {
            if (body.velocity == Vector3.zero)
            {
                if (!m_IsMoving) return;
                
                m_IsMoving = false;
                OnStopMoving?.Invoke();
                return;
            }
            
            if (m_IsMoving) return;

            m_IsMoving = true;
            OnStartMoving?.Invoke();
        }

        private void UpdateAnimator()
        {
            animator.IsWalking = body.velocity != Vector3.zero;
        }

        private static Vector3 GetDirectionNormalized()
        {
            return ServiceLocator
                .Get<GameInput>()
                .GetDirectionNormalized();
        }
    }
}
