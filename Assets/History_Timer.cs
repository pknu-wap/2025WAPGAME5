using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class History_Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed = 0f;    
    private bool isTimerRunning = true;

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

    //시간 멈추는
    public void TimeStop()
    {
        isTimerRunning = false;
    }

    //문제 틀렸을 때
    public void WrongAnswer()
    {
        timeElapsed += 3f;
        UpdateTimerText();  // 바로 UI 반영
    }
}
