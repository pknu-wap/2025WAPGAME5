using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Á¤¹® µµÂø!");

            if (LateManager.Instance != null)
            {
                LateManager.Instance.JudgeLateness();
            }
        }
    }
}
