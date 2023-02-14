using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public abstract class Counter : MonoBehaviour
    {
        [SerializeField] private GameObject selectedVisual;
        [SerializeField] private KitchenObjectSlot slot;


        public bool IsFull => GetIsFull();
        

        public virtual void Interact(Player player)
        {
            
        }
        
        public void Select()
        {
            selectedVisual.SetActive(true);
        }

        public void Deselect()
        {
            selectedVisual.SetActive(false);
        }

        
        protected bool TryPutKitchenObject(KitchenObject kitchenObject)
        {
            return slot.TryPut(kitchenObject);
        }

        protected bool TryGetKitchenObject(out KitchenObject kitchenObject)
        {
            return slot.TryGet(out kitchenObject);
        }

        protected bool TryRemoveKitchenObject(out KitchenObject kitchenObject)
        {
            return slot.TryRemove(out kitchenObject);
        }


        private bool GetIsFull()
        {
            return slot.IsFull;
        }
    }
}