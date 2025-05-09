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

    void Start()
    {
        if (PlayerPrefs.HasKey("MathTime"))
        {
            float mathTime = PlayerPrefs.GetFloat("MathTime");
            Debug.Log("수학 게임 총 플레이 시간: " + mathTime.ToString("F2") + "초");
            PlayerPrefs.DeleteKey("MathTime");
        }
    }
}
