using System;
using EmreBeratKR.ServiceLocator;
using UnityEngine;

public class GameManager : ServiceBehaviour
{
    public static event Action OnBeginInitialize;
    public static event Action OnBeginCountdown;
    public static event Action OnGameStarted;
    public static event Action OnGameOver;
    public static event Action OnBeginTutorial;
    public static event Action OnCompleteTutorial;
    public static event Action OnPaused;
    public static event Action OnUnPaused;
    public static event Action OnUnPauseInput;
    public static event Action<TimerTickArgs> OnGameTimerTick;
    public struct TimerTickArgs
    {
        public float elapsedTime;
        public float remainingTime;


        public float GetTotalTime()
        {
            return elapsedTime + remainingTime;
        }
        
        public float GetElapsedTimeNormalized()
        {
            return Mathf.InverseLerp(0f, GetTotalTime(), elapsedTime);
        }

        public float GetRemainingTimeNormalized()
        {
            return Mathf.InverseLerp(0f, GetTotalTime(), remainingTime);
        }
    }


    public bool IsGameStarted => m_State == State.Playing;
    
    
    private float m_TimerStart;
    private float m_Timer;
    private State m_PausedState;
    private State m_State;


    private void OnEnable()
    {
        ServiceLocator
            .Get<GameInput>()
            .OnConfirm += OnConfirmInput;
        
        ServiceLocator
            .Get<GameInput>()
            .OnPause += OnPauseInput;

        SceneLoader.OnSceneLoaded += OnSceneLoaded;

        UIManager.OnAllMenusClosed += OnAllMenusClosed;
    }

    private void OnDisable()
    {
        ServiceLocator
            .Get<GameInput>()
            .OnConfirm -= OnConfirmInput;
        
        ServiceLocator
            .Get<GameInput>()
            .OnPause -= OnPauseInput;
        
        SceneLoader.OnSceneLoaded -= OnSceneLoaded;
        
        UIManager.OnAllMenusClosed -= OnAllMenusClosed;
    }

    private void Update()
    {
        UpdateState();
    }


    public void Resume()
    {
        OnPauseInput();
    }


    private void OnSceneLoaded(SceneLoader.SceneLoadedArgs args)
    {
        m_State = args.scene switch
        {
            Scene.MainMenu => State.MainMenu,
            _ => State.WaitingForTutorial
        };

        if (m_State == State.WaitingForTutorial)
        {
            OnBeginTutorial?.Invoke();
        }
    }

    private void OnAllMenusClosed()
    {
        UnPause();
    }

    private void OnConfirmInput()
    {
        if (m_State != State.WaitingForTutorial) return;
        
        CompleteTutorial();
    }
    
    private void OnPauseInput()
    {
        switch (m_State)
        {
            case State.Paused:
                OnUnPauseInput?.Invoke();
                break;
            
            case State.WaitingToStart:
                Pause();
                break;
            
            case State.Countdown:
                Pause();
                break;
            
            case State.Playing:
                Pause();
                break;
        }
    }
    

    private void UpdateState()
    {
        switch (m_State)
        {
            case State.WaitingToStart:
                UpdateStateWaitingToStart();
                break;
            
            case State.Countdown:
                UpdateStateCountdown();
                break;
            
            case State.Playing:
                UpdateStatePlaying();
                break;
        }
    }

    private void CompleteTutorial()
    {
        InitializeGame();
        OnCompleteTutorial?.Invoke();
    }
    
    private void InitializeGame()
    {
        m_State = State.WaitingToStart;
        
        OnBeginInitialize?.Invoke();
        
        const float waitToStartTimer = 0.25f;
        SetTimer(waitToStartTimer);
    }

    private void UpdateStateWaitingToStart()
    {
        TickTimer();
        
        if (m_Timer > 0f) return;

        const float countdownTimer = 3f;
        SetTimer(countdownTimer);
        m_State = State.Countdown;
        OnBeginCountdown?.Invoke();
    }
    
    private void UpdateStateCountdown()
    {
        TickTimer();
        
        if (m_Timer > 0f) return;

        const float gameTimer = 60f;
        SetTimer(gameTimer);
        m_State = State.Playing;
        OnGameStarted?.Invoke();
    }
    
    private void UpdateStatePlaying()
    {
        TickTimer();
        
        if (m_Timer > 0f) return;

        m_State = State.GameOver;
        OnGameOver?.Invoke();
    }

    private void SetTimer(float seconds)
    {
        m_TimerStart = seconds;
        m_Timer = seconds;
    }
    
    private void TickTimer()
    {
        m_Timer -= Time.deltaTime;
        OnGameTimerTick?.Invoke(new TimerTickArgs
        {
            elapsedTime = m_TimerStart - m_Timer,
            remainingTime = m_Timer
        });
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        m_PausedState = m_State;
        m_State = State.Paused;
        OnPaused?.Invoke();
    }

    private void UnPause()
    {
        Time.timeScale = 1f;
        m_State = m_PausedState;
        OnUnPaused?.Invoke();
    }


    private enum State
    {
        MainMenu,
        WaitingForTutorial,
        WaitingToStart,
        Countdown,
        Playing,
        GameOver,
        Paused
    }
}