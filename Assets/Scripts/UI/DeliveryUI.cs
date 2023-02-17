using CounterSystem;
using EmreBeratKR.ServiceLocator;
using General;
using UnityEngine;

namespace UI
{
    public class DeliveryUI : MonoBehaviour
    {
        private IngredientListUI[] m_Deliveries;


        private void Awake()
        {
            m_Deliveries = GetComponentsInChildren<IngredientListUI>(true);
            
            ServiceLocator
                .Get<DeliveryCounter>()
                .OnDeliveryQueueChanged += OnDeliveryQueueChanged;
        }

        private void OnDestroy()
        {
            ServiceLocator
                .Get<DeliveryCounter>()
                .OnDeliveryQueueChanged -= OnDeliveryQueueChanged;
        }


        private void OnDeliveryQueueChanged(DeliveryCounter.DeliveryQueueChangedArgs args)
        {
            var i = 0;
            
            foreach (var delivery in args.deliveryQueue)
            {
                m_Deliveries[i].gameObject.SetActive(true);
                m_Deliveries[i].OnIngredientListChanged(new IngredientListChangedArgs
                {
                    name = delivery.name,
                    ingredients = delivery.GetIngredients()
                });

                i += 1;
            }

            for (; i < m_Deliveries.Length; i++)
            {
                m_Deliveries[i].gameObject.SetActive(false);
            }
        }
    }
}