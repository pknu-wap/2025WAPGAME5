using UnityEngine;
using UnityEngine.SceneManagement;

public class MathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Math");
        }
    }
}
