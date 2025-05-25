using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayAfterEnd = 2f;

    void Start()
    {
        StartCoroutine(PlayLoopWithDelay());
    }

    private IEnumerator PlayLoopWithDelay()
    {
        while (true)
        {
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(delayAfterEnd);
        }
    }

    void Update()
    {

    }
}
