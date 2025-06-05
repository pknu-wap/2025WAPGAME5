using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private bool hasTriggered = false; // 이미 실행됐는지 여부 확인용
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("정문 도착!");
            GameManager.Instance.JudgeLateness();
        }
    }
}
