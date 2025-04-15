using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> emotionObjects = new List<GameObject>();

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
    }
}
