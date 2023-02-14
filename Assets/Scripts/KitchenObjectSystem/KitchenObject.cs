using UnityEngine;

namespace KitchenObjectSystem
{
    public class KitchenObject : MonoBehaviour
    {
        private KitchenObjectSlot m_Slot;
        private KitchenObjectSO m_Data;
        
        
        public void SetSlot(KitchenObjectSlot slot)
        {
            m_Slot = slot;
            transform.parent = slot.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    
        public void DestroySelf()
        {
            Destroy(gameObject);
        }


        public static KitchenObject Spawn(KitchenObjectSO data)
        {
            var newKitchenObject = Instantiate(data.Prefab);
            newKitchenObject.m_Data = data;
            return newKitchenObject;
        }
        

        public static implicit operator KitchenObjectSO(KitchenObject kitchenObject)
        {
            return kitchenObject.m_Data;
        }
        
        public static bool operator ==(KitchenObject lhs, KitchenObjectSO rhs)
        {
            return lhs.m_Data == rhs;
        }
    
        public static bool operator !=(KitchenObject lhs, KitchenObjectSO rhs)
        {
            return !(lhs == rhs);
        }
    
        public static bool operator ==(KitchenObjectSO lhs, KitchenObject rhs)
        {
            return lhs == rhs.m_Data;
        }

        public static bool operator !=(KitchenObjectSO lhs, KitchenObject rhs)
        {
            return !(lhs == rhs);
        }
    }
}