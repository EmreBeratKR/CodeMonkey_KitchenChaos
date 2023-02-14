using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public abstract class Counter : MonoBehaviour
    {
        [SerializeField] private GameObject selectedVisual;
        [SerializeField] private KitchenObjectSlot slot;


        protected bool IsFull => GetIsFull();
        

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

        protected void TakeOrGiveKitchenObjectWithPlayer(Player player)
        {
            if (TryGetKitchenObject(out var kitchenObject))
            {
                if (!player.TryPutKitchenObject(kitchenObject)) return;
                
                ClearKitchenObject();
                return;
            }
            
            if (!player.TryGetKitchenObject(out kitchenObject)) return;
            
            if (!TryPutKitchenObject(kitchenObject)) return;
            
            player.ClearKitchenObject();
        }
        
        protected void ClearKitchenObject()
        {
            slot.Clear();
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

        protected void DestroyKitchenObject()
        {
            if (!TryGetKitchenObject(out var kitchenObject)) return;
            
            kitchenObject.DestroySelf();
            ClearKitchenObject();
        }

        protected KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObject)
        {
            if (IsFull) return null;

            var kitchenObj = KitchenObject.Spawn(kitchenObject);
            TryPutKitchenObject(kitchenObj);

            return kitchenObj;
        }


        private bool GetIsFull()
        {
            return slot.IsFull;
        }
    }
}