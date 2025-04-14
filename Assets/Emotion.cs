using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> emotionObjects = new List<GameObject>();

    [SerializeField]
    private ParticleSystem emotionParticle; // 파티클 시스템 연결

    private int lastEmotion = -1;

    private void Update()
    {
        if (GameManager.currentEmotion != lastEmotion)
        {
            UpdateEmotionVisual();
            lastEmotion = GameManager.currentEmotion;
        }
    }

    private void UpdateEmotionVisual()
    {
        for (int i = 0; i < emotionObjects.Count; i++)
        {
            emotionObjects[i].SetActive(i == GameManager.currentEmotion);
        }

        if (emotionParticle != null)
        {
            emotionParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // 기존 파티클 초기화
            emotionParticle.Play(); // 새로 재생
        }
    }
}
