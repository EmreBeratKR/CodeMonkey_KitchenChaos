using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnBeginCountdown;
    public static event Action OnGameStarted;
    public static event Action OnGameOver;
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
    
    
    private float m_TimerStart;
    private float m_Timer;
    private State m_State;


#if UNITY_EDITOR

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        OnBeginCountdown = null;
    }
    
#endif
    

    private void Start()
    {
        const float waitToStartTimer = 1f;
        SetTimer(waitToStartTimer);
    }

    private void Update()
    {
        UpdateState();
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


    private enum State
    {
        WaitingToStart,
        Countdown,
        Playing,
        GameOver
    }
}