using UnityEngine;
using Utils;

namespace CounterSystem
{
    public class DeliveryCounterSound : MonoBehaviour
    {
        [SerializeField] private DeliveryCounter counter;
        [SerializeField] private AudioClip[] successSounds;
        [SerializeField] private AudioClip[] failSounds;


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


        private void OnDeliverySucceed()
        {
            GameAudio.PlayClip(successSounds.Random());
        }

        private void OnDeliveryFailed()
        {
            GameAudio.PlayClip(failSounds.Random());
        }
    }
}