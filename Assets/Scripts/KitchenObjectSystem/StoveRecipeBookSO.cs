using UnityEngine;

namespace KitchenObjectSystem
{
    [CreateAssetMenu]
    public class StoveRecipeBookSO : ScriptableObject
    {
        [SerializeField] private StoveRecipeSO[] recipes;
        
        
        public StoveRecipeSO GetRecipe(KitchenObjectSO input)
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