using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCar : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("��");
            GameManager.currentScene += 1;
        }
    }
}
