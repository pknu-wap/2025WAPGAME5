using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;
    public TextMeshProUGUI scoreText;
    public Transform egg;

    private SpriteRenderer lineRenderer;
    private bool hasFinished = false;

    private List<float> scores = new List<float>();
    void Start()
    {
        lineRenderer = targetLine.GetComponent<SpriteRenderer>();
    }

    public void ClearText()
    {
        hasFinished = false;
        scoreText.text = "";
        scoreText.gameObject.SetActive(false);
    }

    public float CalculateScore(Vector3 eggPosition, bool forcedFail)
    {
        if (hasFinished) return 0f;
        hasFinished = true;
        scoreText.gameObject.SetActive(true);

        float eggBottomY = eggPosition.y;
        float lineTopY = targetLine.position.y + (lineRenderer.bounds.size.y / 2f);

        float score = 0f;

        if (forcedFail)
        {
            scoreText.text = "실패! 너무 늦게 멈췄습니다.\n점수: 0";
            return 0f;
        }

        if (eggBottomY < lineTopY)
        {
            scoreText.text = "실패! 선을 넘었습니다.\n점수: 0";
            return 0f;
        }

        float distanceInCm = (eggBottomY - lineTopY) * 0.1f;
        float maxDistance = 20f; // 20cm 이상은 실패

        if (distanceInCm > maxDistance)
        {
            scoreText.text = $"실패! 20cm 이상 남기고 멈췄습니다.\n점수: 0";
            return 0f;
        }
        else if (Mathf.Approximately(eggBottomY, lineTopY)) 
        {
            score = 100f;
            scoreText.text = $"딱 맞췄습니다!\n점수: 100";
        }
        else
        {
            score = Mathf.Clamp(100f - (distanceInCm / maxDistance) * 100f, 0f, 100f);
            scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score:F0}";
        }

        scores.Add(score);
        return score;
    }

    public float GetAverageScore()
    {
        if (scores.Count == 0) return 0f;
        float sum = 0f;
        foreach (var s in scores)
        {
            sum += s;
        }
        return sum / scores.Count;
    }


    public float GetLineBottomY()
    {
        return targetLine.position.y - (lineRenderer.bounds.size.y / 2f);
    }
}
