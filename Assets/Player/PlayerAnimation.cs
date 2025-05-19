using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator m_anim;

    private bool isRunning;
    private bool isJumping;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public void UpdateAnimationParameters(Vector3 velocity, float horizontalInput, float verticalInput)
    {
        bool hasMovementInput = horizontalInput != 0 || verticalInput != 0;
        isRunning = Input.GetKey(KeyCode.LeftShift) && hasMovementInput;
        isJumping = Input.GetKeyDown(KeyCode.Space);

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        float speed = horizontalVelocity.magnitude;

        bool isMoving = speed > 0.1f;

        m_anim.SetBool("isMoving", isMoving);
        m_anim.SetBool("isRunning", isRunning);
        m_anim.SetBool("isJumping", isJumping);
    }
}
