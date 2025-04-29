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
    

    bool DontMove = false;
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_trs = GetComponent<Transform>();

        m_fLookSensitivity = 5f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!DontMove)
        {
            InputHandle();
            CameraRotation();
            MoveMent();
        }
        else
        {
            m_rigid.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    void FixedUpdate()
    {
        if (!DontMove)
        {            
                 
        }
        else
        {
            //m_rigid.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    void InputHandle()
    {
        m_fHorizontalInput = Input.GetAxisRaw("Horizontal");
        m_fVerticalInput = Input.GetAxisRaw("Vertical");

        m_fMouseXInput = Input.GetAxisRaw("Mouse X");
        m_fMouseYInput = Input.GetAxisRaw("Mouse Y");        
    }

    void MoveMent()
    {
        m_vMoveDirection = (m_trs.right * m_fHorizontalInput + m_trs.forward * m_fVerticalInput).normalized;

        Vector3 vVelocity = m_vMoveDirection * m_fWalkSpeed;

        m_rigid.velocity = new Vector3(vVelocity.x, m_rigid.velocity.y, vVelocity.z);
    }

    void CameraRotation()
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
   
}