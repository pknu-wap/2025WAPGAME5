using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceMeter : MonoBehaviour
{
    public Transform targetLine;
    public TextMeshProUGUI scoreText;

    public void CalculateScore(Vector3 eggPosition)
    {
        float distance = Mathf.Abs(eggPosition.y - targetLine.position.y);
        float maxDistance = 5f;

        float score = Mathf.Max(0f, (1 - distance / maxDistance) * 100f);
        scoreText.text = $"Score: {score:F1}";
    }
}
