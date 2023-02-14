using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class PlatesCounter : Counter
    {
        [SerializeField] private KitchenObjectSO kitchenObject;


        private void Start()
        {
            while (true)
            {
                if (!SpawnAndPutKitchenObject(kitchenObject)) break;
            }
        }


        public override void Interact(Player player)
        {
            if (player.IsFull && !player.ContainsKitchenObject(kitchenObject)) return;
            
            TakeOrGiveKitchenObjectWithPlayer(player);
        }
    }
}