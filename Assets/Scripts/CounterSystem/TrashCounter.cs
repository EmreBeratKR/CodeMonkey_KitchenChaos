using System;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class TrashCounter : Counter
    {
        [SerializeField] private KitchenObjectSO[] ignoredKitchenObjects;
        
        
        public event Action OnFilled;
        
        
        public override void Interact(Player player)
        {
            if (TryDestroyKitchenObjectsFromPlayerPlate(player))
            {
                OnFilled?.Invoke();
                return;
            }
            
            if (IsPlayerContainsIgnoredKitchenObject(player)) return;
            
            if (!player.TryRemoveKitchenObject(out KitchenObject kitchenObject)) return;
            
            kitchenObject.DestroySelf();
            
            OnFilled?.Invoke();
        }


        private bool IsPlayerContainsIgnoredKitchenObject(Player player)
        {
            if (!player.TryGetKitchenObject(out KitchenObject kitchenObject)) return false;

            foreach (var ignoredKitchenObject in ignoredKitchenObjects)
            {
                if (kitchenObject.Data == ignoredKitchenObject) return true;
            }

            return false;
        }

        private bool TryDestroyKitchenObjectsFromPlayerPlate(Player player)
        {
            if (!player.TryGetKitchenObject(out PlateKitchenObject plate)) return false;

            if (plate.IsEmpty) return false;
            
            plate.DestroyAllKitchenObjects();

            return true;
        }
    }
}