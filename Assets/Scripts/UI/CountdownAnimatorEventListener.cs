using System;
using UnityEngine;

namespace UI
{
    public class CountdownAnimatorEventListener : MonoBehaviour
    {
        public event Action onCountdown;


        private void OnCountdown()
        {
            onCountdown?.Invoke();
        }
    }
}