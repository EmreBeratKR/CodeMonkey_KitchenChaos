using CounterSystem;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;


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
    
    
    public static bool operator ==(KitchenObject lhs, KitchenObjectSO rhs)
    {
        return lhs.kitchenObject == rhs;
    }
    
    public static bool operator !=(KitchenObject lhs, KitchenObjectSO rhs)
    {
        return !(lhs == rhs);
    }
    
    public static bool operator ==(KitchenObjectSO lhs, KitchenObject rhs)
    {
        return lhs == rhs.kitchenObject;
    }

    public static bool operator !=(KitchenObjectSO lhs, KitchenObject rhs)
    {
        return !(lhs == rhs);
    }
}