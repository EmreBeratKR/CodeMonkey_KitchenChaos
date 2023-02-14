using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : ServiceBehaviour
{
    private PlayerInputActions.PlayerActions m_PlayerActions;


    public event Action OnInteract;
    

    private void Awake()
    {
        var handler = new PlayerInputActions();
        m_PlayerActions = handler.Player;
       
        m_PlayerActions.Move.Enable();
        
        m_PlayerActions.Interact.Enable();
        m_PlayerActions.Interact.performed += OnInteractPerformed;
    }

    private void OnDestroy()
    {
        m_PlayerActions.Move.Disable();

        m_PlayerActions.Interact.performed -= OnInteractPerformed;
        m_PlayerActions.Interact.Disable();
    }


    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
    

    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }
}