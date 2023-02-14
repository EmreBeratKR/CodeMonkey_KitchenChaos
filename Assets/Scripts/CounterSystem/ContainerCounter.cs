using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class ContainerCounter : Counter
    {
        [SerializeField] private KitchenObjectSO kitchenObject;


        public override void Interact(Player player)
        {
            if (TryTakeKitchenObjectFromPlayer(player)) return;

            TryGiveKitchenObjectToPlayer(player);
        }


        private bool TryTakeKitchenObjectFromPlayer(Player player)
        {
            if (IsFull) return false;

            if (!player.TryGetKitchenObject(out var kitchenObj)) return false;

            if (kitchenObj != kitchenObject) return false;

            if (!player.TryRemoveKitchenObject(out kitchenObj)) return false;
            
            kitchenObj.DestroySelf();

            return true;
        }

        private bool TryGiveKitchenObjectToPlayer(Player player)
        {
            if (player.IsFull) return false;

            if (!TryRemoveKitchenObject(out var kitchenObj))
            {
                kitchenObj = SpawnKitchenObject();
            }

            return player.TryPutKitchenObject(kitchenObj);
        }
        
        private KitchenObject SpawnKitchenObject()
        {
            return Instantiate(kitchenObject.Prefab);
        }
    }
}