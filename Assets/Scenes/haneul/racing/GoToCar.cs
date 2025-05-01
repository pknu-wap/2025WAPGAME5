using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCar : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¹Û");
            GameManager.currentScene += 1;
        }
    }
}
