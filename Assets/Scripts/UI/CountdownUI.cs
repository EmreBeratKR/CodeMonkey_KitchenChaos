using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private GameObject counterMain;
        [SerializeField] private TMP_Text counterField;


        private void OnEnable()
        {
            GameManager.OnBeginCountdown += OnBeginCountdown;
            GameManager.OnGameStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            GameManager.OnBeginCountdown -= OnBeginCountdown;
            GameManager.OnGameStarted -= OnGameStarted;
        }

        private void OnBeginCountdown()
        {
            GameManager.OnGameTimerTick += OnTimerTick;
            counterMain.SetActive(true);
        }

        private void OnGameStarted()
        {
            GameManager.OnGameTimerTick -= OnTimerTick;
            counterMain.SetActive(false);
        }

        private void OnTimerTick(GameManager.TimerTickArgs args)
        {
            counterField.text = Mathf.CeilToInt(args.remainingTime).ToString();
        }
    }
}