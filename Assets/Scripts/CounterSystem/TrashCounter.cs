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
            if (IsPlayerContainsIgnoredKitchenObject(player)) return;
            
            if (!player.TryRemoveKitchenObject(out var kitchenObject)) return;
            
            kitchenObject.DestroySelf();
            
            OnFilled?.Invoke();
        }


        private bool IsPlayerContainsIgnoredKitchenObject(Player player)
        {
            if (!player.TryGetKitchenObject(out var kitchenObject)) return false;

            foreach (var ignoredKitchenObject in ignoredKitchenObjects)
            {
                if (kitchenObject == ignoredKitchenObject) return true;
            }

            return false;
        }
    }
}