using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : ServiceBehaviour
{
    private const string BindingsSaveKey = "Bindings";

    public event Action OnInteract;
    public event Action OnInteractAlternate;
    public event Action OnConfirm;
    public event Action OnPause;


    private static string BindingsAsJson
    {
        get => PlayerPrefs.GetString(BindingsSaveKey);
        set => PlayerPrefs.SetString(BindingsSaveKey, value);
    }
    
    
    private PlayerInputActions.PlayerActions m_PlayerActions;
    private PlayerInputActions m_Actions;
    

    private void Awake()
    {
        m_Actions = new PlayerInputActions();
        m_PlayerActions = m_Actions.Player;

        if (PlayerPrefs.HasKey(BindingsSaveKey))
        {
            m_Actions.LoadBindingOverridesFromJson(BindingsAsJson);
        }
        
        GameManager.OnGameStarted += GameManager_OnGameStarted;
        GameManager.OnGameOver += GameManager_OnGameOver;
        GameManager.OnUnPaused += GameManager_OnUnPaused;

        SceneLoader.OnSceneLoaded += OnSceneLoaded;

        EnableGeneralInputs();
    }

    private void OnDestroy()
    {
        DisablePlayerInputs();
        
        GameManager.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.OnGameOver -= GameManager_OnGameOver;
        GameManager.OnUnPaused -= GameManager_OnUnPaused;
        
        SceneLoader.OnSceneLoaded -= OnSceneLoaded;
        
        DisableGeneralInputs();
    }


    public Vector3 GetDirectionNormalized()
    {
        var value = m_PlayerActions.Move.ReadValue<Vector2>();
        return new Vector3(value.x, 0f, value.y);
    }

    public void Rebind(KeyBinding keyBinding, Action onComplete, Action onCancel)
    {
        DisableGeneralInputs();
        DisablePlayerInputs();

        InputAction inputAction;
        int bindingIndex;
        
        switch (keyBinding)
        {
            default:
            
            case KeyBinding.MoveUp:
                inputAction = m_PlayerActions.Move;
                bindingIndex = 1;
                break;
            
            case KeyBinding.MoveDown:
                inputAction = m_PlayerActions.Move;
                bindingIndex = 2;
                break;
            
            case KeyBinding.MoveLeft:
                inputAction = m_PlayerActions.Move;
                bindingIndex = 3;
                break;
            
            case KeyBinding.MoveRight:
                inputAction = m_PlayerActions.Move;
                bindingIndex = 4;
                break;
            
            case KeyBinding.Interact:
                inputAction = m_PlayerActions.Interact;
                bindingIndex = 0;
                break;
            
            case KeyBinding.InteractAlternate:
                inputAction = m_PlayerActions.InteractAlternate;
                bindingIndex = 0;
                break;
        }
        
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnCancel(callback =>
            {
                callback.Dispose();
                onCancel?.Invoke();
            })
            .OnComplete(callback =>
            {
                callback.Dispose();
                BindingsAsJson = m_Actions.SaveBindingOverridesAsJson();
                EnableGeneralInputs();
                EnablePlayerInputs();
                onComplete?.Invoke();
            })
            .Start();
    }

    public string KeyBindingToKeyName(KeyBinding keyBinding)
    {
        return keyBinding switch
        {
            KeyBinding.MoveUp => m_PlayerActions.Move.bindings[1].ToDisplayString(),
            KeyBinding.MoveDown => m_PlayerActions.Move.bindings[2].ToDisplayString(),
            KeyBinding.MoveLeft => m_PlayerActions.Move.bindings[3].ToDisplayString(),
            KeyBinding.MoveRight => m_PlayerActions.Move.bindings[4].ToDisplayString(),
            KeyBinding.Interact => m_PlayerActions.Interact.bindings[0].ToDisplayString(),
            KeyBinding.InteractAlternate => m_PlayerActions.InteractAlternate.bindings[0].ToDisplayString(),
            _ => "[?]",
        };
    }

    public string KeyBindingToName(KeyBinding keyBinding)
    {
        return keyBinding switch
        {
            KeyBinding.MoveUp => "Move Up",
            KeyBinding.MoveDown => "Move Down",
            KeyBinding.MoveLeft => "Move Left",
            KeyBinding.MoveRight => "Move Right",
            KeyBinding.Interact => "Interact",
            KeyBinding.InteractAlternate => "Interact Alternate",
            _ => "Unknown"
        };
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

    private void GameManager_OnUnPaused()
    {
        if (!ServiceLocator
            .Get<GameManager>()
            .IsGameStarted)
        {
            DisablePlayerInputs();
        }
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
}

public enum KeyBinding
{
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    Interact,
    InteractAlternate
}