using System;
using UnityEngine;

namespace CounterSystem
{
    public class StoveCounterAnimatorEventListener : MonoBehaviour
    {
        public event Action onWarning;


        private void OnWarning()
        {
            onWarning?.Invoke();
        }
    }
}