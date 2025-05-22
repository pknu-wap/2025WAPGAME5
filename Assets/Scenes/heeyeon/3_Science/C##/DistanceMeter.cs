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

        Debug.Log($"[디버그] lineTopY: {lineTopY}, eggBottomY: {eggBottomY}");


        if (forcedFail)
        {
            scoreText.text = "실패! 너무 늦게 멈췄습니다.\n점수: 0";
            return;
        }

        if (eggBottomY < lineTopY)
        {
            scoreText.text = "실패! 선을 넘었습니다.\n점수: 0";
            return;
        }

        float distanceInCm = (eggBottomY - lineTopY) * 0.1f;

        float maxDistance = 20f; // 20cm 넘으면 실패 처리
        float score;

        if (distanceInCm > maxDistance)
        {
            score = 0f;
            scoreText.text = $"실패! 20cm 이상 남기고 멈췄습니다.\n점수: 0";
        }

        else if(eggBottomY == lineTopY)
        {
            scoreText.text = $"딱 맞췄습니다!\n점수: 100";
        }

        else
        {
            score = Mathf.Clamp(100f - (distanceInCm / maxDistance) * 100f, 0f, 100f);
            scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score:F0}";
        }
            
    
    }

    public float GetLineBottomY()
    {
        return targetLine.position.y - (lineRenderer.bounds.size.y / 2f);
    }
}
