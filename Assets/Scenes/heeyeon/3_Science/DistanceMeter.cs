using UnityEngine;
using TMPro;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;
    public TextMeshProUGUI scoreText;
    public Transform egg;

    private SpriteRenderer lineRenderer;
    private bool hasFinished = false;

    void Start()
    {
        lineRenderer = targetLine.GetComponent<SpriteRenderer>();
    }

    public void CalculateScore(Vector3 eggPosition, bool forcedFail)
    {
        if (hasFinished) return;
        hasFinished = true;

        float eggBottomY = eggPosition.y;
        float lineTopY = targetLine.position.y + (lineRenderer.bounds.size.y / 2f);

        float rawDistance = lineTopY - eggBottomY;

        if (forcedFail)
        {
            scoreText.text = "실패! 너무 늦게 멈췄습니다.\n점수: 0";
            return;
        }

        if (rawDistance <= 0f)
        {
            scoreText.text = "실패! 선을 넘었습니다.\n점수: 0";
            return;
        }

        float distanceInCm = rawDistance * 0.1f;

        float score = 0f;
        if (distanceInCm <= 0.1f) score = 100f;
        else if (distanceInCm <= 1f) score = 95f;
        else if (distanceInCm <= 2f) score = 80f;
        else if (distanceInCm <= 3f) score = 60f;
        else if (distanceInCm <= 4f) score = 40f;
        else if (distanceInCm <= 5f) score = 20f;
        else score = 0f;

        if (score == 100f)
            scoreText.text = $"딱 맞췄습니다!\n점수: 100";
        else
            scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score}";
    }

    public float GetLineBottomY()
    {
        return targetLine.position.y - (lineRenderer.bounds.size.y / 2f);
    }
}
