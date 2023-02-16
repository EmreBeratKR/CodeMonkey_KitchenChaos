using System;
using General;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class CuttingCounter : Counter, IProgressProvider
    {
        [SerializeField] private CuttingRecipeBookSO recipeBook;


        public event Action<ProgressChangedArgs> OnProgressChanged;
        public event Action OnCut;
        

        private CuttingRecipeSO m_CurrentRecipe;
        private int m_CurrentCut;
        
        
        public override void Interact(Player player)
        {
            if (TryCombineWithPlate(player)) return;

            TakeOrGiveKitchenObjectWithPlayer(player);

            if (IsEmpty)
            {
                m_CurrentRecipe = null;
                ResetProgress();
            }
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
            if (!TryGetKitchenObject(out KitchenObject kitchenObject)) return false;

            var recipe = recipeBook.GetRecipe(kitchenObject.Data);
            
            if (!recipe) return false;

            if (!m_CurrentRecipe)
            {
                m_CurrentRecipe = recipe;
                ResetProgress();
            }

            m_CurrentCut += 1;
            OnProgressChanged?.Invoke(new ProgressChangedArgs
            {
                progressNormalized = Mathf.InverseLerp(0f, recipe.CutCount, m_CurrentCut)
            });

            if (m_CurrentCut >= m_CurrentRecipe.CutCount)
            {
                DestroyKitchenObject();
                SpawnAndPutKitchenObject(recipe.Output);
                m_CurrentRecipe = null;
                ResetProgress();
            }

            return true;
        }

        private void ResetProgress()
        {
            m_CurrentCut = 0;
            OnProgressChanged?.Invoke(new ProgressChangedArgs
            {
                progressNormalized = 0f
            });
        }
    }
}