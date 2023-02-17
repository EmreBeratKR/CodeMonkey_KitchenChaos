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
            if (TryCombineWithPlate(player)) return;

            if (!player.TryGetKitchenObject(out PlateKitchenObject plate) && !player.IsEmpty) return;
            
            if (plate && !plate.IsEmpty) return;
            
            TakeOrGiveKitchenObjectWithPlayer<KitchenObject>(player);
        }
    }
}