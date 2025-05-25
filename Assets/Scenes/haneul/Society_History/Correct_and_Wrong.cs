using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
