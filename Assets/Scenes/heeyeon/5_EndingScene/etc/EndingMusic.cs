using UnityEngine;

public class EndingMusicPlayer : MonoBehaviour
{
    public AudioSource endingAudioSource;

    public void PlayEndingMusic()
    {
        if (!endingAudioSource.isPlaying)
        {
            endingAudioSource.Play();
        }
    }
}
