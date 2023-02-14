using UnityEngine;

namespace KitchenObjectSystem
{
    [CreateAssetMenu]
    public class KitchenObjectSO : ScriptableObject
    {
        [field: SerializeField] public KitchenObject Prefab { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}