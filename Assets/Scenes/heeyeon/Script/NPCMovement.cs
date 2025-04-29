using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isnotStudent = true;
    public Animator NPC;

    private Vector3 moveDirection;

    void Start()
    {
        NPC = GetComponent<Animator>();
        moveDirection = isnotStudent ? Vector3.forward : Vector3.back;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        NPC.applyRootMotion = false;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
