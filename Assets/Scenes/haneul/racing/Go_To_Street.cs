using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_To_Street : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            Debug.Log("���");
            GameManager.currentScene += 1;
            GameManager.currentMission += 1;
        }
    }
}
