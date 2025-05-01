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

        // ���� ���� ������Ʈ�� Ȱ��ȭ
        for (int i = 0; i < emotionObjects.Count; i++)
        {
            emotionObjects[i].SetActive(i == lastEmotion);
        }

        yield return new WaitForSeconds(3f);

        // ���� �ʱ�ȭ
        GameManager.currentEmotion = 0;
        lastEmotion = 0;

        for (int i = 0; i < emotionObjects.Count; i++)
        {
            emotionObjects[i].SetActive(i == 0);
        }

        isChangeEmotion = false;
    }
}
