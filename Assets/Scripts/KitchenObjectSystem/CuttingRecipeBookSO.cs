using UnityEngine;

namespace KitchenObjectSystem
{
    [CreateAssetMenu]
    public class CuttingRecipeBookSO : ScriptableObject
    {
        [SerializeField] private CuttingRecipeSO[] recipes;


        public CuttingRecipeSO GetRecipe(KitchenObjectSO input)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Input != input) continue;

                return recipe;
            }

            return null;
        }
    }
}