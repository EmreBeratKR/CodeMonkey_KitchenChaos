using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenObjectSystem
{
    [CreateAssetMenu]
    public class CompleteRecipeBookSO : ScriptableObject, IEnumerable<CompleteRecipe>
    {
        [SerializeField] private CompleteRecipe[] recipes;



        public CompleteRecipe GetRandomRecipe()
        {
            return recipes[Random.Range(0, recipes.Length)];
        }
        
        public IEnumerator<CompleteRecipe> GetEnumerator()
        {
            return recipes.Cast<CompleteRecipe>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}