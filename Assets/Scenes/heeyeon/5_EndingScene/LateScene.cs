using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LateScene : MonoBehaviour
{
    public float moveSpeed = 2f;
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
