using UnityEngine;

namespace KitchenObjectSystem
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO data;


        public KitchenObjectSO Data => data;
        
        
        private KitchenObjectSlot m_Slot;
        
        
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
            return newKitchenObject;
        }
    }
}