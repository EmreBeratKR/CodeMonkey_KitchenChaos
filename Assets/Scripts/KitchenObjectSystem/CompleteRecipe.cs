using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace KitchenObjectSystem
{
    public class CompleteRecipe : MonoBehaviour, IEnumerable<KitchenObject>
    {
        [SerializeField] private KitchenObject[] ingredients;


        public bool Contains(KitchenObjectSO kitchenObject)
        {
            foreach (var input in ingredients)
            {
                if (input.Data == kitchenObject) return true;
            }

            return false;
        }

        public Vector3 GetLocalPosition(KitchenObjectSO kitchenObject)
        {
            foreach (var input in ingredients)
            {
                if (input.Data == kitchenObject) return input.transform.localPosition;
            }

            return default;
        }
        
        public IEnumerator<KitchenObject> GetEnumerator()
        {
            return ingredients.Cast<KitchenObject>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


#if UNITY_EDITOR

        [Button]
        private void AutoFindIngredients()
        {
            ingredients = GetComponentsInChildren<KitchenObject>();
        }
        
#endif
    }
}