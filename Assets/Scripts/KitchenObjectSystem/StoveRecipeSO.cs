using UnityEngine;

namespace KitchenObjectSystem
{
    [CreateAssetMenu]
    public class StoveRecipeSO : ScriptableObject
    {
        [field: SerializeField] public KitchenObjectSO Input { get; private set; }
        [field: SerializeField] public KitchenObjectSO Output { get; private set; }
        [field: SerializeField] public float Seconds { get; private set; }
        [field: SerializeField] public bool IsBurning { get; private set; }
    }
}