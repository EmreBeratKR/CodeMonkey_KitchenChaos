using EmreBeratKR.ServiceLocator;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController body;
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 25f;


    private void Update()
    {
        var direction = GetDirectionNormalized();
        
        UpdatePosition(direction);
        UpdateRotation(direction);
        UpdateAnimator(direction);
    }


    private void UpdatePosition(Vector3 direction)
    {
        var motion = direction * (moveSpeed * Time.deltaTime);
        body.Move(motion);
    }

    private void UpdateRotation(Vector3 direction)
    {
        var forward = Vector3.Slerp(body.transform.forward, direction, Time.deltaTime * rotationSpeed);
        body.transform.forward = forward;
    }

    private void UpdateAnimator(Vector3 direction)
    {
        animator.IsWalking = direction != Vector3.zero;
    }
    
    private static Vector3 GetDirectionNormalized()
    {
        return ServiceLocator
            .Get<GameInput>()
            .GetDirectionNormalized();
    }
}
