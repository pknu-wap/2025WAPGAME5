using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_To_Street : MonoBehaviour
{
    private float startTime;      
    private bool timerStarted = false; 

    void Start()
    {
        startTime = Time.time;
        timerStarted = true;
        Debug.Log("타이머 시작");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car") && timerStarted)
        {
            float elapsedTime = Time.time - startTime;
            Debug.Log($"골목 도착 - 소요 시간: {elapsedTime:F2}초");

            GameManager.currentScene += 1;
            GameManager.currentMission += 1;

            timerStarted = false; 
        }
    }
}
