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
            Debug.Log("���� ���� �� �÷��� �ð�: " + mathTime.ToString("F2") + "��");
            PlayerPrefs.DeleteKey("MathTime");
        }
    }
}
