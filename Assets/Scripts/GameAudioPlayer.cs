using UnityEngine;

public class GameAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        GameManager.OnPaused += OnGamePaused;
        GameManager.OnUnPaused += OnGameUnPaused;
    }

    private void OnDestroy()
    {
        GameManager.OnPaused -= OnGamePaused;
        GameManager.OnUnPaused -= OnGameUnPaused;
    }
    
    
    private void OnGamePaused()
    {
        audioSource.Pause();
    }

    private void OnGameUnPaused()
    {
        audioSource.UnPause();
    }


    public void Play(float volume = 1f)
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}