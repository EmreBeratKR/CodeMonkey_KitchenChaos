using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsWalkingID = Animator.StringToHash("IsWalking");


    [SerializeField] private Animator animator;


    public bool IsWalking
    {
        get => GetIsWalking();
        set => SetIsWalking(value);
    }
    

    private void SetIsWalking(bool isWalking)
    {
        animator.SetBool(IsWalkingID, isWalking);
    }

    private bool GetIsWalking()
    {
        return animator.GetBool(IsWalkingID);
    }
}