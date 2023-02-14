using UnityEngine;

namespace CounterSystem
{
    public class TrashCounterAnimator : MonoBehaviour
    {
        private static readonly int FillID = Animator.StringToHash("Fill");


        [SerializeField] private TrashCounter counter;
        [SerializeField] private Animator animator;


        private void OnEnable()
        {
            counter.OnFilled += OnCounterFilled;
        }

        private void OnDisable()
        {
            counter.OnFilled -= OnCounterFilled;
        }


        private void OnCounterFilled()
        {
            TriggerFill();
        }


        private void TriggerFill()
        {
            animator.SetTrigger(FillID);
        }
    }
}