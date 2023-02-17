using UnityEngine;
using Utils;

namespace CounterSystem
{
    public class TrashCounterSound : MonoBehaviour
    {
        [SerializeField] private TrashCounter counter;
        [SerializeField] private AudioClip[] fillSounds;


        private void OnEnable()
        {
            counter.OnFilled += OnFilled;
        }

        private void OnDisable()
        {
            counter.OnFilled -= OnFilled;
        }


        private void OnFilled()
        {
            GameAudio.PlayClip(fillSounds.Random());
        }
    }
}