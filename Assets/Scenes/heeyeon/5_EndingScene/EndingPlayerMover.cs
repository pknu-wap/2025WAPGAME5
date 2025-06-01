using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EndingPlayerMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public AudioSource audioSource;
    public AudioClip countUpSound;

    [System.Serializable]
    public struct SubjectScoreUI
    {
        public string label;
        public TMP_Text scoreText;
        public string playerPrefKey;// 점수 불러오기 이름
    }

    public List<SubjectScoreUI> subjects = new List<SubjectScoreUI>();

    void Start()
    {
        StartCoroutine(ShowAllScoresSequentially());
    }

    IEnumerator ShowAllScoresSequentially()
    {
        foreach (var subject in subjects)
        {
            int score = PlayerPrefs.GetInt(subject.playerPrefKey, 0);
            yield return StartCoroutine(CountUpScore(subject.scoreText, score, subject.label));
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CountUpScore(TMP_Text targetText, int targetScore, string label)
    {
        int displayedScore = 0;
        float duration = 2f;
        float elapsed = 0f;
        float soundDuration = 1.7f;

        int lastDisplayedScore = -1;

        if (countUpSound != null)
        {
            audioSource.clip = countUpSound;
            audioSource.Play();
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            displayedScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, t));

            if (displayedScore != lastDisplayedScore)
            {
                targetText.text = $"{label} 점수: {displayedScore} 점";
                lastDisplayedScore = displayedScore;
            }

            if (elapsed >= soundDuration && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            yield return null;
        }

        targetText.text = $"{label} 점수: {targetScore} 점";
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
