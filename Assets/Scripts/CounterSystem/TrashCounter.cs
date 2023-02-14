using System;
using PlayerSystem;

namespace CounterSystem
{
    public class TrashCounter : Counter
    {
        public event Action OnFilled;
        
        
        public override void Interact(Player player)
        {
            if (!player.TryRemoveKitchenObject(out var kitchenObject)) return;
            
            kitchenObject.DestroySelf();
            
            OnFilled?.Invoke();
        }
    }
}