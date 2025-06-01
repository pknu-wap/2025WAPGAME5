using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_to_Car : MonoBehaviour
{

    public static bool isGameEnded = false;
    private void OnTriggerEnter(Collider other)
    {
        if (Interaction.isGameEnded && other.CompareTag("Player") && gameObject.CompareTag("GORACING"))
        {
            SceneManager.LoadScene("haneul_racing");
        }
    }
}
