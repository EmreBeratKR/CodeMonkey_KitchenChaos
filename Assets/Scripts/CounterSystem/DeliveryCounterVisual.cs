using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CounterSystem
{
    public class DeliveryCounterVisual : MonoBehaviour
    {
        private static readonly string SuccessID = "Success";
        private static readonly string FailID = "Fail";
        
        
        [SerializeField] private DeliveryCounter counter;
        [SerializeField] private Animator animator;
        [SerializeField] private TMP_Text messageField;
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private DeliveryFeedbackVisualData 
            successVisualData,
            failVisualData;


        private void OnEnable()
        {
            counter.OnDeliverySucceed += OnDeliverySucceed;
            counter.OnDeliveryFailed += OnDeliveryFailed;
        }

        private void OnDisable()
        {
            counter.OnDeliverySucceed -= OnDeliverySucceed;
            counter.OnDeliveryFailed -= OnDeliveryFailed;
        }

        private void OnDeliverySucceed(DeliveryCounter.DeliverySucceedArgs args)
        {
            ApplyVisualData(successVisualData);
            TriggerSuccess();
        }
        
        private void OnDeliveryFailed()
        {
            ApplyVisualData(failVisualData);
            TriggerFail();
        }


        private void ApplyVisualData(DeliveryFeedbackVisualData data)
        {
            messageField.text = data.message;
            background.color = data.backgroundColor;
            icon.sprite = data.icon;
        }
        
        private void TriggerSuccess()
        {
            animator.SetTrigger(SuccessID);
        }

        private void TriggerFail()
        {
            animator.SetTrigger(FailID);
        }
        
        
        
        [Serializable]
        private struct DeliveryFeedbackVisualData
        {
            public Color backgroundColor;
            [ResizableTextArea]
            public string message;
            public Sprite icon;
        }
    }
}