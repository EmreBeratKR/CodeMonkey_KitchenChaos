using UnityEngine;

namespace CounterSystem
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        private static readonly int CutID = Animator.StringToHash("Cut");


        [SerializeField] private CuttingCounter counter;
        [SerializeField] private Animator animator;


        private void OnEnable()
        {
            counter.OnCut += OnCounterCut;
        }

        private void OnDisable()
        {
            counter.OnCut -= OnCounterCut;
        }


        private void OnCounterCut()
        {
            TriggerCut();
        }


        private void TriggerCut()
        {
            animator.SetTrigger(CutID);
        }
    }
}