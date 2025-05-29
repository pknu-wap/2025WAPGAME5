using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    public RectTransform uiElement;
    public float duration = 2f;
    public float moveX = 100f;
    public bool ismove = false;

    public AudioSource audioSource;

    public void Move()
    {
        if (!ismove)
        {
            ismove = true;
            StartCoroutine(MoveUIElement());
            audioSource.Play();
        }
    }

    void update()
    {

    }

    IEnumerator MoveUIElement()
    {
        Vector2 startPos = uiElement.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(moveX, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            uiElement.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;

            // x 값이 1800 이상이면 로그 출력
            if (uiElement.anchoredPosition.x >= 1600f)
            {
                Debug.Log("도착");
                break;
            }
        }

        uiElement.anchoredPosition = targetPos;
        ismove = false;
    }
}
