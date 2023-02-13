using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController body;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 25f;


    private void Update()
    {
        var direction = GetDirectionNormalized();
        var motion = direction * (moveSpeed * Time.deltaTime);
        body.Move(motion);

        var forward = Vector3.Slerp(body.transform.forward, direction, Time.deltaTime * rotationSpeed);
        body.transform.forward = forward;
    }


    private Vector3 GetDirectionNormalized()
    {
        var direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.z = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.z = -1f;
        }

        return direction.normalized;
    }
}
