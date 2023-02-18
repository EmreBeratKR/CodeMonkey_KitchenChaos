using System;
using System.Collections.Generic;
using EmreBeratKR.ServiceLocator;
using KitchenObjectSystem;
using PlayerSystem;
using UnityEngine;

namespace CounterSystem
{
    [ServiceSceneLoad(ServiceSceneLoadMode.Destroy)]
    public class DeliveryCounter : Counter, IService
    {
        [SerializeField] private CompleteRecipeBookSO recipeBook;
        [SerializeField] private int deliveryQueueSize = 3;


        public event Action OnDeliverySucceed;
        public event Action OnDeliveryFailed;
        public event Action<DeliveryQueueChangedArgs> OnDeliveryQueueChanged;
        public struct DeliveryQueueChangedArgs
        {
            public Queue<CompleteRecipe> deliveryQueue;
        }


        private readonly Queue<CompleteRecipe> m_DeliveryQueue = new();


        private void OnEnable()
        {
            GameManager.OnGameStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= OnGameStarted;
        }


        public override void Interact(Player player)
        {
            if (IsFull)
            {
                TryGiveKitchenObjectToPlayer(player);
                return;
            }
            
            TakeOrGiveKitchenObjectWithPlayer<PlateKitchenObject>(player);
            
            CheckDelivery();
        }


        private void OnGameStarted()
        {
            GetInitialDeliveries();
        }
        

        private void GetInitialDeliveries()
        {
            for (var i = 0; i < deliveryQueueSize; i++)
            {
                var randomRecipe = GetRandomRecipe();
                m_DeliveryQueue.Enqueue(randomRecipe);
            }
            
            RaiseDeliveryQueueChanged();
        }
        
        private void CheckDelivery()
        {
            if (!TryGetKitchenObject(out PlateKitchenObject plate)) return;

            var deliveredIngredients = plate.GetIngredients();
            var currentRecipe = m_DeliveryQueue.Peek();

            if (!currentRecipe.Matches(deliveredIngredients))
            {
                OnDeliveryFailed?.Invoke();
                return;
            }

            plate.DestroyAllKitchenObjects();
            m_DeliveryQueue.Dequeue();
            m_DeliveryQueue.Enqueue(GetRandomRecipe());
            RaiseDeliveryQueueChanged();

            OnDeliverySucceed?.Invoke();
        }

        private void RaiseDeliveryQueueChanged()
        {
            OnDeliveryQueueChanged?.Invoke(new DeliveryQueueChangedArgs
            {
                deliveryQueue = m_DeliveryQueue
            });
        }
        
        private CompleteRecipe GetRandomRecipe()
        {
            return recipeBook.GetRandomRecipe();
        }
    }
}