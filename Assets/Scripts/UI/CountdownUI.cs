using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownUI : MonoBehaviour
    {
        private const string Go = "GO!";
        private static readonly int CountID = Animator.StringToHash("Count");
        
        
        [SerializeField] private TMP_Text counterField;
        [SerializeField] private Animator animator;


        private void OnEnable()
        {
            GameManager.OnBeginCountdown += OnBeginCountdown;
            GameManager.OnGameStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            GameManager.OnBeginCountdown -= OnBeginCountdown;
            GameManager.OnGameStarted -= OnGameStarted;
            GameManager.OnGameTimerTick -= OnTimerTick;
        }

        private void OnBeginCountdown()
        {
            GameManager.OnGameTimerTick += OnTimerTick;
        }

        private void OnGameStarted()
        {
            GameManager.OnGameTimerTick -= OnTimerTick;
            
            counterField.text = Go;
            TriggerCount();
        }

        private void OnTimerTick(GameManager.TimerTickArgs args)
        {
            var oldText = counterField.text;
            var newText = Mathf.CeilToInt(args.remainingTime).ToString();
            counterField.text = newText;

            if (newText != oldText)
            {
                TriggerCount();
            }
        }


        private void TriggerCount()
        {
            animator.SetTrigger(CountID);
        }
    }
}