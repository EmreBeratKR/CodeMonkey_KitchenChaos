using PlayerSystem;

namespace CounterSystem
{
    public class ClearCounter : Counter
    {
        public override void Interact(Player player)
        {
            TakeOrGiveKitchenObjectWithPlayer(player);
        }
    }
}