using UnityEngine;
using Utils;

namespace CounterSystem
{
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter counter;
        [SerializeField] private GameAudioPlayer audioPlayer;
        [SerializeField] private AudioClip[] warningSounds;
        [SerializeField] private StoveCounterAnimatorEventListener eventListener;


        private void OnEnable()
        {
            counter.OnBeginCooking += OnBeginCooking;
            counter.OnStopCooking += OnStopCooking;

            eventListener.onWarning += OnWarning;
        }

        private void OnDisable()
        {
            counter.OnBeginCooking -= OnBeginCooking;
            counter.OnStopCooking -= OnStopCooking;
            
            eventListener.onWarning -= OnWarning;
        }


        private void OnBeginCooking()
        {
            audioPlayer.Play(0.2f);
        }

        private void OnStopCooking()
        {
            audioPlayer.Stop();
        }

        private void OnWarning()
        {
            GameAudio.PlayClip(warningSounds.Random());
        }
    }
}