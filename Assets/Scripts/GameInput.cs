using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : ServiceBehaviour
{
    private PlayerInputActions.PlayerActions m_PlayerActions;


    public event Action OnInteract;
    public event Action OnInteractAlternate;
    

    private void Awake()
    {
        var handler = new PlayerInputActions();
        m_PlayerActions = handler.Player;
        
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        Disable();
        
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }


    private void OnGameStarted()
    {
        Enable();
    }

    private void OnGameOver()
    {
        Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
    
    private void OnInteractAlternatePerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke();
    }


    private void Enable()
    {
        m_PlayerActions.Move.Enable();
        
        m_PlayerActions.Interact.Enable();
        m_PlayerActions.Interact.performed += OnInteractPerformed;
        
        m_PlayerActions.InteractAlternate.Enable();
        m_PlayerActions.InteractAlternate.performed += OnInteractAlternatePerformed;
    }

    private void Disable()
    {
        m_PlayerActions.Move.Disable();

        m_PlayerActions.Interact.performed -= OnInteractPerformed;
        m_PlayerActions.Interact.Disable();

        m_PlayerActions.InteractAlternate.performed -= OnInteractAlternatePerformed;
        m_PlayerActions.InteractAlternate.Disable();
    }
    

    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }
}