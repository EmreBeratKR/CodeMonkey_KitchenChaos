using UnityEngine;

namespace CounterSystem
{
    public class ContainerCounterVisual : MonoBehaviour
    {
        private static readonly int OpenCloseID = Animator.StringToHash("OpenClose");


        [SerializeField] private ContainerCounter counter;
        [SerializeField] private Animator animator;


        private void OnEnable()
        {
            counter.OnOpenClosed += OnCounterOpenClosed;
        }

        private void OnDisable()
        {
            counter.OnOpenClosed -= OnCounterOpenClosed;
        }


        private void OnCounterOpenClosed()
        {
            TriggerOpenClose();
        }


        private void TriggerOpenClose()
        {
            animator.SetTrigger(OpenCloseID);
        }
    }
}