using System;
using General;
using UnityEngine;

namespace KitchenObjectSystem
{
    public class PlateKitchenObject : KitchenObject, IIngredientListProvider
    {
        [SerializeField] private CompleteRecipeBookSO recipeBook;

        
        public event Action<IngredientListChangedArgs> OnIngredientListChanged;
        

        public bool IsEmpty => GetIsEmpty();
        
        
        private KitchenObjectSlot[] m_Slots;
        private KitchenObjectSO[] m_IngredientsBuffer;


        private void Awake()
        {
            m_Slots = GetComponentsInChildren<KitchenObjectSlot>();
            m_IngredientsBuffer = new KitchenObjectSO[m_Slots.Length];
        }


        public bool TryCombineWithKitchenObject(KitchenObject kitchenObject)
        {
            CompleteRecipe foundRecipe = null;
            
            foreach (var recipe in recipeBook)
            {
                if (!IsValidRecipe(recipe)) continue;
            
                if (!CanCombine(kitchenObject, recipe)) continue;

                foundRecipe = recipe;
                break;
            }

            if (!foundRecipe) return false;
            
            CombineWithKitchenObject(kitchenObject, foundRecipe);

            return true;
        }

        public void DestroyAllKitchenObjects()
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.TryRemove(out KitchenObject kitchenObject)) continue;
                
                kitchenObject.DestroySelf();
            }
            
            RaiseIngredientListChanged();
        }


        private void RaiseIngredientListChanged()
        {
            for (var i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i].TryGet(out KitchenObject kitchenObject))
                {
                    m_IngredientsBuffer[i] = kitchenObject.Data;
                    continue;
                }

                m_IngredientsBuffer[i] = null;
            }
            
            OnIngredientListChanged?.Invoke(new IngredientListChangedArgs
            {
                ingredients = m_IngredientsBuffer
            });
        }
        
        private bool IsValidRecipe(CompleteRecipe recipe)
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.TryGet(out KitchenObject kitchenObj)) continue;

                if (!recipe.Contains(kitchenObj.Data)) return false;
            }

            return true;
        }
        
        private bool CanCombine(KitchenObject kitchenObject, CompleteRecipe recipe)
        {
            foreach (var ingredient in recipe)
            {
                if (Contains(ingredient.Data)) continue;
                
                if (ingredient.Data == kitchenObject.Data) return true;
            }

            return false;
        }
        
        private void CombineWithKitchenObject(KitchenObject kitchenObject, CompleteRecipe recipe)
        {
            TryPutKitchenObject(kitchenObject);

            foreach (var slot in m_Slots)
            {
                if (!slot.TryGet(out KitchenObject kitchenObj)) continue;

                var localPosition = recipe.GetLocalPosition(kitchenObj.Data);
                kitchenObj.transform.localPosition = localPosition;
            }
            
            RaiseIngredientListChanged();
        }

        private bool TryPutKitchenObject(KitchenObject kitchenObject)
        {
            foreach (var slot in m_Slots)
            {
                if (slot.TryPut(kitchenObject)) return true;
            }

            return false;
        }

        private bool Contains(KitchenObjectSO kitchenObject)
        {
            foreach (var slot in m_Slots)
            {
                if (slot.Contains(kitchenObject)) return true;
            }

            return false;
        }

        private bool GetIsEmpty()
        {
            foreach (var slot in m_Slots)
            {
                if (!slot.IsEmpty) return false;
            }

            return true;
        }
    }
}