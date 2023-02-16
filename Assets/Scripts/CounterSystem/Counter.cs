using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public abstract class Counter : MonoBehaviour
    {
        [SerializeField] private GameObject selectedVisual;


        protected bool IsFull => GetIsFull();
        protected bool IsEmpty => GetIsEmpty();
        
        
        private KitchenObjectSlot[] m_Slots;


        protected virtual void Awake()
        {
            m_Slots = GetComponentsInChildren<KitchenObjectSlot>();
        }


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
            if (TryGetKitchenObject(out KitchenObject kitchenObject))
            {
                if (player.TryPutKitchenObject(kitchenObject))
                {
                    ClearKitchenObject(kitchenObject);
                    return;
                }
            }
            
            if (!player.TryGetKitchenObject(out kitchenObject)) return;
            
            if (!TryPutKitchenObject(kitchenObject)) return;
            
            player.ClearKitchenObject();
        }
        
        protected bool TryCombineWithPlate(Player player)
        {
            if (player.TryGetKitchenObject(out PlateKitchenObject plate))
            {
                if (!TryGetKitchenObject(out KitchenObject kitchenObject)) return false;

                if (!plate.TryCombineWithKitchenObject(kitchenObject)) return false;
                
                ClearKitchenObject(kitchenObject);
                
                return true;
            }

            if (!TryGetKitchenObject(out plate)) return false;

            if (!player.TryGetKitchenObject(out KitchenObject kitchenObj)) return false;

            if (!plate.TryCombineWithKitchenObject(kitchenObj)) return false;
            
            player.ClearKitchenObject();
            
            return true;
        }
        
        protected void ClearKitchenObject(KitchenObject kitchenObject)
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.TryGet(out KitchenObject kitchenObj)) continue;
                
                if (kitchenObj != kitchenObject) continue;
                
                slot.Clear();
            }
        }

        protected bool TryPutKitchenObject(KitchenObject kitchenObject)
        {
            foreach (var slot in m_Slots)
            {
                if (slot.TryPut(kitchenObject)) return true;
            }

            return false;
        }

        protected bool TryGetKitchenObject<T>(out T kitchenObject)
            where T : KitchenObject
        {
            for (var i = m_Slots.Length - 1; i >= 0; i--)
            {
                if (m_Slots[i].TryGet(out kitchenObject)) return true;
            }

            kitchenObject = null;
            return false;
        }

        protected bool TryRemoveKitchenObject<T>(out T kitchenObject)
            where T : KitchenObject
        {
            for (var i = m_Slots.Length - 1; i >= 0; i--)
            {
                if (m_Slots[i].TryRemove(out kitchenObject)) return true;
            }

            kitchenObject = null;
            return false;
        }

        protected void DestroyKitchenObject()
        {
            if (!TryGetKitchenObject(out KitchenObject kitchenObject)) return;
            
            kitchenObject.DestroySelf();
            ClearKitchenObject(kitchenObject);
        }

        protected KitchenObject SpawnAndPutKitchenObject(KitchenObjectSO kitchenObject)
        {
            if (IsFull) return null;

            var kitchenObj = KitchenObject.Spawn(kitchenObject);
            TryPutKitchenObject(kitchenObj);

            return kitchenObj;
        }


        private bool GetIsFull()
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.IsFull) return false;
            }

            return true;
        }

        private bool GetIsEmpty()
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.IsEmpty) return false;
            }

            return true;
        }
    }
}