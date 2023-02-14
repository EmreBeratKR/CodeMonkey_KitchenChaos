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
            if (player.TryGetKitchenObject(out var kitchenObj))
            {
                if (kitchenObj != kitchenObject) return;
                
                kitchenObj.DestroySelf();
                player.ClearKitchenObject();
                OnOpenClosed?.Invoke();
                return;
            }
            
            kitchenObj = SpawnKitchenObject(kitchenObject);
            player.TryPutKitchenObject(kitchenObj);
            ClearKitchenObject();
            OnOpenClosed?.Invoke();
        }
    }
}