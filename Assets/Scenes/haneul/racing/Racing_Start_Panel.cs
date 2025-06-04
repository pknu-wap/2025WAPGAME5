using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racing_Start_Panel : MonoBehaviour
{


    void Start()
    {
        Time.timeScale = 0f;
        GameManager.isPausingInGame = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.isPausingInGame = false;
        }

    }
}
