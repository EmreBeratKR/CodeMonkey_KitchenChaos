using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : ServiceBehaviour
{
    private PlayerInputActions.PlayerActions m_PlayerActions;


    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnConfirm;
    public event Action OnPause;
    

    private void Awake()
    {
        var handler = new PlayerInputActions();
        m_PlayerActions = handler.Player;
        
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnGameOver += GameManager_OnGameOver;

        SceneLoader.OnSceneLoaded += OnSceneLoaded;

        EnableGeneralInputs();
    }

    private void OnDestroy()
    {
        DisablePlayerInputs();
        
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnGameOver -= GameManager_OnGameOver;
        
        SceneLoader.OnSceneLoaded -= OnSceneLoaded;
        
        DisableGeneralInputs();
    }


    private void OnSceneLoaded(SceneLoader.SceneLoadedArgs args)
    {
        DisablePlayerInputs();
    }
    
    private void GameManager_OnGameStarted()
    {
        EnablePlayerInputs();
    }

    private void GameManager_OnGameOver()
    {
        DisablePlayerInputs();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
    
    private void OnInteractAlternatePerformed(InputAction.CallbackContext context)
    {
        OnInteractAlternate?.Invoke();
    }

    private void OnConfirmPerformed(InputAction.CallbackContext context)
    {
        OnConfirm?.Invoke();
    }
    
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        OnPause?.Invoke();
    }


    private void EnablePlayerInputs()
    {
        m_PlayerActions.Move.Enable();
        
        m_PlayerActions.Interact.Enable();
        m_PlayerActions.Interact.performed += OnInteractPerformed;
        
        m_PlayerActions.InteractAlternate.Enable();
        m_PlayerActions.InteractAlternate.performed += OnInteractAlternatePerformed;
    }

    private void DisablePlayerInputs()
    {
        m_PlayerActions.Move.Disable();

        m_PlayerActions.Interact.performed -= OnInteractPerformed;
        m_PlayerActions.Interact.Disable();

        m_PlayerActions.InteractAlternate.performed -= OnInteractAlternatePerformed;
        m_PlayerActions.InteractAlternate.Disable();
    }

    private void EnableGeneralInputs()
    {
        m_PlayerActions.Confirm.Enable();
        m_PlayerActions.Confirm.performed += OnConfirmPerformed;
        
        m_PlayerActions.Pause.Enable();
        m_PlayerActions.Pause.performed += OnPausePerformed;
    }

    private void DisableGeneralInputs()
    {
        m_PlayerActions.Confirm.Enable();
        m_PlayerActions.Confirm.performed -= OnConfirmPerformed;
        
        m_PlayerActions.Pause.Disable();
        m_PlayerActions.Pause.performed -= OnPausePerformed;
    }
    

    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }
}