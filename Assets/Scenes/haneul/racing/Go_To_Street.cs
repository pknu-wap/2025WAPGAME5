using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Go_To_Street : MonoBehaviour
{
    private float startTime;
    private bool timerStarted = false;
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        startTime = Time.time;
        timerStarted = true;
        Debug.Log("타이머 시작");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car") && timerStarted)
        {
            float elapsedTime = Time.time - startTime;

            Debug.Log($"2단계 자동차 트리거:{elapsedTime:F2}초");

            GameManager.currentMission += 1;
            timerStarted = false;
            GameManager.Instance.racingTime = elapsedTime;

            StartCoroutine(FadeOut("heeyeon"));
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
