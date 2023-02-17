using System;
using KitchenObjectSystem;

namespace General
{
    public interface IIngredientListProvider
    {
        public event Action<IngredientListChangedArgs> OnIngredientListChanged;
    }

    public struct IngredientListChangedArgs
    {
        public KitchenObjectSO[] ingredients;
    }
}