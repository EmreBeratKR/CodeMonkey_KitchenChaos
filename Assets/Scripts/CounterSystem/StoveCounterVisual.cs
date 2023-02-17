using General;
using UnityEngine;

namespace CounterSystem
{
    public class StoveCounterVisual : MonoBehaviour
    {
        private static readonly int IsWarning = Animator.StringToHash("IsWarning");
        
        
        [SerializeField] private GameObject[] cookingVisuals;
        [SerializeField] private StoveCounter counter;
        [SerializeField] private Animator animator;
        [SerializeField, Range(0f, 1f)] private float warningProgressNormalized;


        private void OnEnable()
        {
            counter.OnBeginCooking += OnCounterBeginCooking;
            counter.OnStopCooking += OnCounterStopCooking;
            counter.OnProgressChanged += OnCounterProgressChanged;
        }

        private void OnDisable()
        {
            counter.OnBeginCooking -= OnCounterBeginCooking;
            counter.OnStopCooking -= OnCounterStopCooking;
            counter.OnProgressChanged -= OnCounterProgressChanged;
        }

        
        private void OnCounterBeginCooking()
        {
            SetActiveCookingVisuals(true);
        }
        
        private void OnCounterStopCooking()
        {
            SetActiveCookingVisuals(false);
        }

        private void OnCounterProgressChanged(ProgressChangedArgs args)
        {
            var isWarningProgress = args.progressNormalized >= warningProgressNormalized;
            SetIsWarning(isWarningProgress && counter.IsBurning);
        }


        private void SetIsWarning(bool value)
        {
            animator.SetBool(IsWarning, value);
        }
        
        private void SetActiveCookingVisuals(bool value)
        {
            foreach (var cookingVisual in cookingVisuals)
            {
                cookingVisual.SetActive(value);
            }
        }
    }
}