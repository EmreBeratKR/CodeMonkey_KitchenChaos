using PlayerSystem;

namespace CounterSystem
{
    public class TrashCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (!player.TryRemoveKitchenObject(out var kitchenObject)) return;
            
            kitchenObject.DestroySelf();
        }
    }
}