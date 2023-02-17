using UnityEngine;

public static class GameAudio
{
    public static void PlayClip(AudioClip clip, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
    }
}