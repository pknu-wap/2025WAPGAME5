using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Manager>().EndGame();

            Mouse mouseController = FindObjectOfType<Mouse>();
            if (mouseController != null)
            {
                mouseController.LockCursor();
            }
        }
    }
}
