using KitchenObjectSystem;
using PlayerSystem;

namespace CounterSystem
{
    public class ClearCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (TryCombineWithPlate(player)) return;
            
            TakeOrGiveKitchenObjectWithPlayer<KitchenObject>(player);
        }
    }
}