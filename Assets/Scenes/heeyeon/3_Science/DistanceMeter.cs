using UnityEngine;
using TMPro;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;  
    public TextMeshProUGUI scoreText; 

    public void CalculateScore(Vector3 eggPosition)
    {
        float rawDistance = Mathf.Abs(eggPosition.y - targetLine.position.y);
        float distanceInCm = rawDistance * 0.1f * 0.5f;
        float maxDistance = 0.5f;
        float score = Mathf.Max(0f, (1f - rawDistance / maxDistance) * 100f);

        scoreText.text = $"{distanceInCm:F1}cm 남기고 멈췄습니다!\n점수: {score:F1}";
    }
}
