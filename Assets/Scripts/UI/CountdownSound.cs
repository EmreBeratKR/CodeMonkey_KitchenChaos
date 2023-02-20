using EmreBeratKR.ServiceLocator;
using UnityEngine;
using Utils;

namespace UI
{
    public class CountdownSound : MonoBehaviour
    {
        [SerializeField] private CountdownAnimatorEventListener eventListener;
        [SerializeField] private AudioClip countdownEndSound;
        [SerializeField] private AudioClip[] countdownSounds;


        private void OnEnable()
        {
            eventListener.onCountdown += OnCountdown;
        }

        private void OnDisable()
        {
            eventListener.onCountdown -= OnCountdown;
        }


        private void OnCountdown()
        {
            if (ServiceLocator.Get<GameManager>().IsGameStarted)
            {
                GameAudio.PlayClip(countdownEndSound);
                return;
            }
            
            GameAudio.PlayClip(countdownSounds.Random());
        }
    }
}