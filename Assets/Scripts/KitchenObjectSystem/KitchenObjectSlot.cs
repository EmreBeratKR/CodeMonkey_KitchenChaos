using UnityEngine;

namespace KitchenObjectSystem
{
    public class KitchenObjectSlot : MonoBehaviour
    {
        private KitchenObject m_KitchenObject;


        public bool IsFull => GetIsFull();
        public bool IsEmpty => GetIsEmpty();


        public void Clear()
        {
            m_KitchenObject = null;
        }
        
        public bool TryPut(KitchenObject kitchenObject)
        {
            if (IsFull) return false;

            if (!kitchenObject) return false;

            m_KitchenObject = kitchenObject;
            kitchenObject.SetSlot(this);

            return true;
        }

        public bool TryGet<T>(out T kitchenObject)
            where T : KitchenObject
        {
            kitchenObject = m_KitchenObject as T;
            return kitchenObject;
        }

        public bool TryRemove<T>(out T kitchenObject)
            where T : KitchenObject
        {
            kitchenObject = (T) m_KitchenObject;

            if (!kitchenObject) return false;
            
            m_KitchenObject = null;
            return true;
        }

        public bool Contains(KitchenObjectSO kitchenObject)
        {
            if (!TryGet(out KitchenObject kitchenObj)) return false;

            return kitchenObj.Data == kitchenObject;
        }


        private bool GetIsFull()
        {
            return m_KitchenObject;
        }

        private bool GetIsEmpty()
        {
            return !m_KitchenObject;
        }
    }
}