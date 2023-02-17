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
        [SerializeField] private int recipeQueueSize = 3;


        public event Action<DeliveryQueueChangedArgs> OnDeliveryQueueChanged;
        public struct DeliveryQueueChangedArgs
        {
            public Queue<CompleteRecipe> deliveryQueue;
        }


        private readonly Queue<CompleteRecipe> m_DeliveryQueue = new();
        

        private void Start()
        {
            for (var i = 0; i < recipeQueueSize; i++)
            {
                var randomRecipe = GetRandomRecipe();
                m_DeliveryQueue.Enqueue(randomRecipe);
            }
            
            RaiseDeliveryQueueChanged();
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


        private void CheckDelivery()
        {
            if (!TryGetKitchenObject(out PlateKitchenObject plate)) return;

            var deliveredIngredients = plate.GetIngredients();
            var currentRecipe = m_DeliveryQueue.Peek();

            if (!currentRecipe.Matches(deliveredIngredients))
            {
                Debug.Log("wrong order!");
                return;
            }

            plate.DestroyAllKitchenObjects();
            m_DeliveryQueue.Dequeue();
            m_DeliveryQueue.Enqueue(GetRandomRecipe());
            RaiseDeliveryQueueChanged();

            Debug.Log("correct order!");
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