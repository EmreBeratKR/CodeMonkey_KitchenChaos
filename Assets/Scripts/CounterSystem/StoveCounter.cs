using System;
using General;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    public class StoveCounter : Counter, IProgressProvider
    {
        [SerializeField] private StoveRecipeBookSO recipeBook;
        
        
        public event Action<ProgressChangedArgs> OnProgressChanged;
        public event Action OnBeginCooking;
        public event Action OnStopCooking;


        public bool IsBurning => GetIsBurning();
        

        private float m_ElapsedSeconds;
        private StoveRecipeSO m_Recipe;
        private State m_State;
        
        
        public override void Interact(Player player)
        {
            TakeOrGiveKitchenObjectWithPlayer(player);
        }


        private void Update()
        {
            if (!TryCook())
            {
                if (m_State != State.Cooking) return;

                ResetProgress();
                m_State = State.Idle;
                OnStopCooking?.Invoke();
                
                return;
            }

            if (m_State == State.Cooking) return;
            
            m_State = State.Cooking;
            OnBeginCooking?.Invoke();
        }

        private bool TryCook()
        {
            if (!TryGetKitchenObject(out var kitchenObject))
            {
                m_Recipe = null;
                return false;
            }

            m_Recipe = recipeBook.GetRecipe(kitchenObject);

            if (!m_Recipe) return false;

            MakeProgress();

            if (m_ElapsedSeconds >= m_Recipe.Seconds)
            {
                ResetProgress();
                CompleteRecipe(m_Recipe);
            }

            return true;
        }

        private void MakeProgress()
        {
            m_ElapsedSeconds += Time.deltaTime;
            OnProgressChanged?.Invoke(new ProgressChangedArgs
            {
                progressNormalized = GetProgressNormalized()
            });
        }

        private void ResetProgress()
        {
            m_ElapsedSeconds = 0f;
            OnProgressChanged?.Invoke(new ProgressChangedArgs
            {
                progressNormalized = 0f
            });
        }

        private void CompleteRecipe(StoveRecipeSO recipe)
        {
            DestroyKitchenObject();
            SpawnKitchenObject(recipe.Output);
            m_State = State.Idle;
            OnStopCooking?.Invoke();
        }

        private float GetProgressNormalized()
        {
            if (!m_Recipe) return 0f;
            
            return Mathf.InverseLerp(0f, m_Recipe.Seconds, m_ElapsedSeconds);
        }

        private bool GetIsBurning()
        {
            return m_Recipe.IsBurning;
        }
        
        
        
        private enum State
        {
            Idle,
            Cooking
        }
    }
}