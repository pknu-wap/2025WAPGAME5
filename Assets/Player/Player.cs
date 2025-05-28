using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody m_rigid;
    Transform m_trs;

    [SerializeField] private float m_fWalkSpeed;
    private Vector3 m_vMoveDirection;

    [SerializeField] private Camera m_camera;
    private float m_fCurrentCamRotationX;
    private float m_fCurrentCamRotationY;
    [SerializeField] private float m_fLookSensitivity;
    [SerializeField] private float m_fMaxLookAngle;

    private float m_fHorizontalInput;
    private float m_fVerticalInput;

    private float m_fMouseXInput;
    private float m_fMouseYInput;

    private bool DontMove = false;
    private PlayerAnimation m_animController;

    public void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_trs = GetComponent<Transform>();

        m_fLookSensitivity = 5f;

        m_animController = GetComponent<PlayerAnimation>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        if (!DontMove)
        {
            InputHandle();
            CameraRotation();
            MoveMent();
            m_animController.UpdateAnimationParameters(m_rigid.velocity, m_fHorizontalInput, m_fVerticalInput);
        }
        else
        {
            m_rigid.velocity = Vector3.zero;
        }
    }

    public void InputHandle()
    {
        m_fHorizontalInput = Input.GetAxisRaw("Horizontal");
        m_fVerticalInput = Input.GetAxisRaw("Vertical");

        m_fMouseXInput = Input.GetAxisRaw("Mouse X");
        m_fMouseYInput = Input.GetAxisRaw("Mouse Y");
    }

    public void MoveMent()
    {
        m_vMoveDirection = (m_trs.right * m_fHorizontalInput + m_trs.forward * m_fVerticalInput).normalized;

        float speed = m_fWalkSpeed;
        Vector3 vVelocity = m_vMoveDirection * speed;

        m_rigid.velocity = new Vector3(vVelocity.x, m_rigid.velocity.y, vVelocity.z);
    }

    public void CameraRotation()
    {
        m_fCurrentCamRotationY += (m_fMouseXInput * m_fLookSensitivity);
        m_fCurrentCamRotationX -= (m_fMouseYInput * m_fLookSensitivity);
        m_fCurrentCamRotationX = Mathf.Clamp(m_fCurrentCamRotationX, -m_fMaxLookAngle, m_fMaxLookAngle);

        m_trs.rotation = Quaternion.Euler(0, m_fCurrentCamRotationY, 0);
        m_camera.transform.rotation = Quaternion.Euler(m_fCurrentCamRotationX, m_fCurrentCamRotationY, 0);
    }

    public void SetDontMove(bool _transition)
    {
        DontMove = _transition;
    }

    public bool GetDontMove()
    {
        return DontMove;
    }

    public void FixCameraRotation(Quaternion rot)
    {
        m_camera.transform.rotation = rot;

        float xAngle = rot.eulerAngles.x;
        if (xAngle > 180f) xAngle -= 360f;

        m_fCurrentCamRotationX = Mathf.Clamp(xAngle, -m_fMaxLookAngle, m_fMaxLookAngle);
        m_fCurrentCamRotationY = rot.eulerAngles.y;
    }
}
