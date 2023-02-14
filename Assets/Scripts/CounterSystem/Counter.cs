using KitchenObjectSystem;
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

        public virtual void InteractAlternate(Player player)
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

        protected void TakeOrGiveInteraction(Player player)
        {
            if (TryTakeKitchenObjectFromPlayer(player)) return;

            TryGiveKitchenObjectToPlayer(player);
        }


        private bool TryTakeKitchenObjectFromPlayer(Player player)
        {
            if (IsFull) return false;
            
            if (!player.TryRemoveKitchenObject(out var kitchenObject)) return false;
            
            return TryPutKitchenObject(kitchenObject);
        }

        private bool TryGiveKitchenObjectToPlayer(Player player)
        {
            if (player.IsFull) return false;

            if (!TryRemoveKitchenObject(out var kitchenObject)) return false;

            return player.TryPutKitchenObject(kitchenObject);
        }
        
        private bool GetIsFull()
        {
            return slot.IsFull;
        }
    }
}