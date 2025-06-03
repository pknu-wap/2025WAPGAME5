using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class History_Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeElapsed = 0f;
    private bool isTimerRunning = true;

    public AudioSource audioSource; 
    public AudioClip correctClip;   
    public AudioClip wrongClip;     

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + timeElapsed.ToString("F1") + "s";
    }

    public void TimeStop()
    {
        isTimerRunning = false;
    }

    public void WrongAnswer()
    {
        timeElapsed += 3f;
        UpdateTimerText();

        audioSource.PlayOneShot(wrongClip);
    }

    public void Correct()
    {
        UpdateTimerText();
        
        audioSource.PlayOneShot(correctClip);
    }
}
