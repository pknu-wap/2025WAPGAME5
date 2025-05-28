using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Á¤¹® µµÂø!");
            GameManager.Instance.JudgeLateness();
        }
    }
}
