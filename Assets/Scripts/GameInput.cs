using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : ServiceBehaviour
{
    private PlayerInputActions.PlayerActions m_PlayerActions;


    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnPause;
    

    private void Awake()
    {
        var handler = new PlayerInputActions();
        m_PlayerActions = handler.Player;
        
        GameManager.OnBeginInitialize += GameManager_OnBeginInitialize;
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDestroy()
    {
        Disable();

        GameManager.OnBeginInitialize -= GameManager_OnBeginInitialize;
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }


    private void GameManager_OnBeginInitialize()
    {
        Disable();
    }
    
    private void GameManager_OnGameStarted()
    {
        Enable();
    }

    private void GameManager_OnGameOver()
    {
        Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
    
    private void OnInteractAlternatePerformed(InputAction.CallbackContext context)
    {
        OnInteractAlternate?.Invoke();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        OnPause?.Invoke();
    }


    private void Enable()
    {
        m_PlayerActions.Move.Enable();
        
        m_PlayerActions.Interact.Enable();
        m_PlayerActions.Interact.performed += OnInteractPerformed;
        
        m_PlayerActions.InteractAlternate.Enable();
        m_PlayerActions.InteractAlternate.performed += OnInteractAlternatePerformed;

        m_PlayerActions.Pause.Enable();
        m_PlayerActions.Pause.performed += OnPausePerformed;
    }

    private void Disable()
    {
        m_PlayerActions.Move.Disable();

        m_PlayerActions.Interact.performed -= OnInteractPerformed;
        m_PlayerActions.Interact.Disable();

        m_PlayerActions.InteractAlternate.performed -= OnInteractAlternatePerformed;
        m_PlayerActions.InteractAlternate.Disable();
        
        m_PlayerActions.Pause.Disable();
        m_PlayerActions.Pause.performed -= OnPausePerformed;
    }
    

    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }
}