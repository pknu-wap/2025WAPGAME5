using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> emotionObjects = new List<GameObject>();

    private int lastEmotion = -1;
    private bool isChangeEmotion = false;

    private void Update()
    {
        if (!isChangeEmotion && GameManager.currentEmotion != lastEmotion)
        {
            StartCoroutine(HandleEmotionChange());
        }
    }

    private IEnumerator HandleEmotionChange()
    {
        isChangeEmotion = true;

        lastEmotion = GameManager.currentEmotion;

        // 현재 감정 오브젝트만 활성화
        for (int i = 0; i < emotionObjects.Count; i++)
        {
            emotionObjects[i].SetActive(i == lastEmotion);
        }

        yield return new WaitForSeconds(3f);

        // 감정 초기화
        GameManager.currentEmotion = 0;
        lastEmotion = 0;

        for (int i = 0; i < emotionObjects.Count; i++)
        {
            emotionObjects[i].SetActive(i == 0);
        }

        isChangeEmotion = false;
    }
}
