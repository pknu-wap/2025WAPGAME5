using UnityEngine;
using TMPro;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;
    public TextMeshProUGUI scoreText;
    public Transform egg;

    private SpriteRenderer eggRenderer;
    private SpriteRenderer lineRenderer;

    private bool hasFinished = false;

    void Start()
    {
        eggRenderer = egg.GetComponent<SpriteRenderer>();
        lineRenderer = targetLine.GetComponent<SpriteRenderer>();
    }

    public void CalculateScore(Vector3 eggPosition)
    {
        float eggTopY = eggPosition.y + (eggRenderer.bounds.size.y / 2f);
        float eggBottomY = eggPosition.y - (eggRenderer.bounds.size.y / 2f);

        float lineTopY = targetLine.position.y + (lineRenderer.bounds.size.y / 2f);
        float lineBottomY = targetLine.position.y - (lineRenderer.bounds.size.y / 2f);

        float minY = lineBottomY + (eggRenderer.bounds.size.y / 2f);
        if (eggPosition.y < minY)
        {
            egg.position = new Vector3(eggPosition.x, minY, eggPosition.z);
            eggBottomY = minY - (eggRenderer.bounds.size.y / 2f);
        }

        if (eggTopY > lineTopY)
        {
            scoreText.text = "실패! 선을 넘었습니다.";
            hasFinished = true;
            return;
        }

        float rawDistance = lineTopY - eggBottomY;
        float maxDistance = 0.5f;
        float distanceInCm = rawDistance * 100f;
        float score = Mathf.Clamp01(1f - rawDistance / maxDistance) * 100f;

        if (Mathf.Approximately(rawDistance, 0f))
        {
            scoreText.text = $"딱 맞췄습니다!\n점수: 100.0";
        }
        else
        {
            scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score:F1}";
        }

        hasFinished = true;
    }
}
