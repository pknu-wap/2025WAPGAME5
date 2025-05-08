using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isnotStudent = true;
    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = isnotStudent ? transform.forward : -transform.forward;
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
