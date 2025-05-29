using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Correct_and_Wrong : MonoBehaviour
{
    public float scaleSpeed = 1f;
    public float rotateSpeed = 40f;
    void Start()
    {
        Destroy(gameObject, 0.7f);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) //숫자 1 왼쪽의 ~키
        {
            PlayerPrefs.SetInt("ReturnedFromHistory", 1);
            SceneManager.LoadScene("ClassRoom");
        }
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
