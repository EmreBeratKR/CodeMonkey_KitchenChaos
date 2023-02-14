using System;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class CuttingCounter : Counter
    {
        [SerializeField] private CuttingRecipeBookSO recipeBook;


        public event Action OnCut;
        

        private CuttingRecipeSO m_CurrentRecipe;
        private int m_CurrentCut;
        
        
        public override void Interact(Player player)
        {
            TakeOrGiveInteraction(player);
        }

        public override void InteractAlternate(Player player)
        {
            if (TryPerformCut())
            {
                OnCut?.Invoke();
            }
        }


        private bool TryPerformCut()
        {
            if (!TryGetKitchenObject(out var kitchenObject)) return false;

            var recipe = recipeBook.GetRecipe(kitchenObject);
            
            if (!recipe) return false;

            if (!m_CurrentRecipe)
            {
                m_CurrentRecipe = recipe;
                m_CurrentCut = 0;
            }

            m_CurrentCut += 1;

            if (m_CurrentCut >= m_CurrentRecipe.CutCount)
            {
                ClearKitchenObject();
                kitchenObject.DestroySelf();
                var output = SpawnRecipeOutput(recipe.Output);
                TryPutKitchenObject(output);
                m_CurrentRecipe = null;
                m_CurrentCut = 0;
            }

            return true;
        }

        private static KitchenObject SpawnRecipeOutput(KitchenObjectSO kitchenObject)
        {
            return KitchenObject.Spawn(kitchenObject);
        }
    }
}