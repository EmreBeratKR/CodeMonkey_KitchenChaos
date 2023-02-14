using PlayerSystem;

namespace CounterSystem
{
    public class ClearCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (TryTakeKitchenObjectFromPlayer(player)) return;

            TryGiveKitchenObjectToPlayer(player);
        }
        
        
        private bool TryTakeKitchenObjectFromPlayer(Player player)
        {
            if (IsFull) return false;
            
            if (!player.TryRemoveKitchenObject(out var kitchenObject)) return false;
            
            return TryPutKitchenObject(kitchenObject);
        }

        private bool TryGiveKitchenObjectToPlayer(Player player)
        {
            if (player.IsFull) return false;

            if (!TryRemoveKitchenObject(out var kitchenObject)) return false;

            return player.TryPutKitchenObject(kitchenObject);
        }
    }
}