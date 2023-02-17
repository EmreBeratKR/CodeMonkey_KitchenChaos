using UnityEngine;

public class GameAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;


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