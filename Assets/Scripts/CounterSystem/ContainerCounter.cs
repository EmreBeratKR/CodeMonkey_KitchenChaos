using System;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class ContainerCounter : Counter
    {
        [SerializeField] private KitchenObjectSO kitchenObject;


        public event Action OnOpenClosed; 
        

        public override void Interact(Player player)
        {
            if (player.TryGetKitchenObject(out KitchenObject kitchenObj))
            {
                if (kitchenObj.Data != kitchenObject) return;
                
                kitchenObj.DestroySelf();
                player.ClearKitchenObject();
                OnOpenClosed?.Invoke();
                return;
            }
            
            kitchenObj = KitchenObject.Spawn(kitchenObject);
            player.TryPutKitchenObject(kitchenObj);
            ClearKitchenObject(kitchenObj);
            OnOpenClosed?.Invoke();
        }
    }
}