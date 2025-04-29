using System.Collections;
using UnityEngine;

public class Car_to_school : MonoBehaviour
{
    public float acceleration = 30f;
    public float maxSpeed = 15f;
    public float turnSpeed = 100f;
    public float driftFactor = 0.95f;
    private Rigidbody rb;

    [SerializeField] private bool isRide = false;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    [Header("Wheel Settings")]
    public float wheelRadius = 0.3f;
    public float steerAngle = 30f; // 앞바퀴 조향 최대 각도

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || isRide)
        {
            isRide = false;
        }

        if (!isRide) return;

        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // 전진/후진 가속
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        // 조향 (속도 있을 때만)
        if (rb.velocity.magnitude > 0.1f)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * turn));
        }

        // 드리프트 처리
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x *= driftFactor;
        rb.velocity = transform.TransformDirection(localVelocity);

        UpdateWheelRotation(moveInput);
        UpdateSteering(turnInput);
    }

    // 바퀴가 굴러가는 회전
    void UpdateWheelRotation(float moveInput)
    {
        float rotationAmount = (rb.velocity.magnitude / (2 * Mathf.PI * wheelRadius)) * 360f * Time.fixedDeltaTime * Mathf.Sign(moveInput);

        frontLeftWheel.Rotate(Vector3.right, rotationAmount);
        frontRightWheel.Rotate(Vector3.right, rotationAmount);
        backLeftWheel.Rotate(Vector3.right, rotationAmount);
        backRightWheel.Rotate(Vector3.right, rotationAmount);
    }

    // 앞바퀴 조향 (좌우 회전)
    void UpdateSteering(float turnInput)
    {
        float angle = turnInput * steerAngle;
        frontLeftWheel.localRotation = Quaternion.Euler(0f, angle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, angle, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("충돌");
            if (Input.GetKeyDown(KeyCode.F))
            {
                isRide = true;
            }
        }
    }
}
