using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Car_to_school : MonoBehaviour
{
    public float acceleration = 30f;
    public float maxSpeed = 15f;
    public float turnSpeed = 100f;
    public float driftFactor = 0.95f;
    public GameObject player;
    public GameObject carCamera;
    public GameObject point;
    public GameObject CarKEY;
    public GameObject Potal;
    private Rigidbody rb;

    [SerializeField] private bool isRide = false;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    [Header("Wheel Settings")]
    public float wheelRadius = 0.3f;
    public float steerAngle = 30f;

    [Header("UI")]
    [SerializeField] private GameObject ridePanel;

    private bool isCollidedWithPlayer = false;
    private bool hasPanelShown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ridePanel.SetActive(false);
    }

    void Update()
    {
        if (isCollidedWithPlayer && !isRide && Input.GetKeyDown(KeyCode.F))
        {
            isRide = true;
            ridePanel.SetActive(false);
            Time.timeScale = 1f;

            
            player.SetActive(false);
            carCamera.SetActive(true);
            point.SetActive(false);
            CarKEY.SetActive(true);
            Potal.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (!isRide) return;

        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // 브레이크
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity *= 0.983f; // 감속률 
        }
        else if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        if (rb.velocity.magnitude > 0.1f)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * turn));
        }

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x *= driftFactor;
        rb.velocity = transform.TransformDirection(localVelocity);

        UpdateWheelRotation(moveInput);
        UpdateSteering(turnInput);
    }

    void UpdateWheelRotation(float moveInput)
    {
        float rotationAmount = (rb.velocity.magnitude / (2 * Mathf.PI * wheelRadius)) * 360f * Time.fixedDeltaTime * Mathf.Sign(moveInput);

        frontLeftWheel.Rotate(Vector3.right, rotationAmount);
        frontRightWheel.Rotate(Vector3.right, rotationAmount);
        backLeftWheel.Rotate(Vector3.right, rotationAmount);
        backRightWheel.Rotate(Vector3.right, rotationAmount);
    }

    void UpdateSteering(float turnInput)
    {
        float angle = turnInput * steerAngle;
        frontLeftWheel.localRotation = Quaternion.Euler(0f, angle, 0f);
        frontRightWheel.localRotation = Quaternion.Euler(0f, angle, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasPanelShown && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 첫 충돌");
            ridePanel.SetActive(true);
            Time.timeScale = 0f;
            isCollidedWithPlayer = true;
            hasPanelShown = true; // 한 번 표시한 것으로 기록
        }
    }

    public void ApplySpeedBoost(float speedAmount, float accelAmount, float duration)
    {
        Debug.Log("가감코루틴작동");
        StartCoroutine(SpeedBoostRoutine(speedAmount, accelAmount, duration));
    }

    private IEnumerator SpeedBoostRoutine(float speedAmount, float accelAmount, float duration)
    {
        float originalMaxSpeed = 80f;
        float originalAcceleration = 30f;

        maxSpeed = speedAmount;
        acceleration = accelAmount;

        Vector3 currentDir = rb.velocity.normalized;
        
        rb.velocity = currentDir * speedAmount;
        

        yield return new WaitForSeconds(duration);

        maxSpeed = originalMaxSpeed;
        acceleration = originalAcceleration;
    }
}
