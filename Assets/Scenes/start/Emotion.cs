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

    public void ChangeEmotion(int emotion)
    {
        if (!isChangeEmotion && emotion != lastEmotion)
        {
            GameManager.currentEmotion = emotion;
            StartCoroutine(HandleEmotionChange());
        }
    }

    private IEnumerator HandleEmotionChange()
    {
        isChangeEmotion = true;

        lastEmotion = GameManager.currentEmotion;

        //  감정 카운트 
        if (lastEmotion == 1) // Happy
        {
            Debug.Log("+ 기분 좋음");
            int happyCount = PlayerPrefs.GetInt("Face_Happy_Count", 0);
            PlayerPrefs.SetInt("Face_Happy_Count", happyCount + 1);
        }
        else if (lastEmotion == 2) // Angry
        {
            Debug.Log("- 기분 나쁨");
            int angryCount = PlayerPrefs.GetInt("Face_angry_Count", 0);
            PlayerPrefs.SetInt("Face_angry_Count", angryCount + 1);
        }

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
