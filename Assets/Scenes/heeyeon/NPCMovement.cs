using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isnotStudent = true;

    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = isnotStudent ? Vector3.forward : Vector3.back;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
