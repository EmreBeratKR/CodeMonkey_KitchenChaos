using UnityEngine;
using Utils;

namespace CounterSystem
{
    public class CuttingCounterSound : MonoBehaviour
    {
        [SerializeField] private CuttingCounter counter;
        [SerializeField] private AudioClip[] cutSounds;


        private void OnEnable()
        {
            counter.OnCut += OnCut;
        }

        private void OnDisable()
        {
            counter.OnCut -= OnCut;
        }


        private void OnCut()
        {
            GameAudio.PlayClip(cutSounds.Random());
        }
    }
}