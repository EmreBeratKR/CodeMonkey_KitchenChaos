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


        public KitchenObjectSO[] GetIngredients()
        {
            return ingredients
                .Select(ingredient => ingredient.Data)
                .ToArray();
        }
        
        public bool Contains(KitchenObjectSO kitchenObject)
        {
            foreach (var input in ingredients)
            {
                if (input.Data == kitchenObject) return true;
            }

            return false;
        }

        public bool Matches(KitchenObjectSO[] ingredientList)
        {
            foreach (var ingredient in ingredients)
            {
                if (!ingredientList.Contains(ingredient.Data)) return false;
            }

            return true;
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