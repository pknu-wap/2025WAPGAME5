using UnityEngine;
using TMPro;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;  
    public TextMeshProUGUI scoreText;
    public Transform egg; 

    public void CalculateScore(Vector3 eggPosition)
    {
        float eggBottomY = eggPosition.y - (egg.GetComponent<SpriteRenderer>().bounds.size.y / 2f); // 달걀은 아래 기준
        float topOfLineY = targetLine.position.y + (targetLine.GetComponent<SpriteRenderer>().bounds.size.y / 2f); // 라인은 윗 선

        float rawDistance = Mathf.Abs(eggBottomY - topOfLineY);

        float distanceInCm = rawDistance * 0.1f * 0.5f;
        float maxDistance = 0.5f;
        float score = Mathf.Max(0f, (1f - rawDistance / maxDistance) * 100f);

        scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score:F1}";
    }
}
