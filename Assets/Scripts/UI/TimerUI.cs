using System;
using General;
using UnityEngine;

namespace UI
{
    public class TimerUI : MonoBehaviour, IProgressProvider
    {
        public event Action<ProgressChangedArgs> OnProgressChanged;


        private void OnEnable()
        {
            GameManager.OnGameStarted += OnGameStarted;
            GameManager.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= OnGameStarted;
            GameManager.OnGameOver -= OnGameOver;
        }


        private void OnGameStarted()
        {
            GameManager.OnGameTimerTick += OnTimerTick;
        }

        private void OnGameOver()
        {
            GameManager.OnGameTimerTick -= OnTimerTick;
        }

        private void OnTimerTick(GameManager.TimerTickArgs args)
        {
            OnProgressChanged?.Invoke(new ProgressChangedArgs
            {
                progressNormalized = args.GetRemainingTimeNormalized()
            });
        }
    }
}