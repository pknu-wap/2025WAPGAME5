using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // Animator 연결
    private Rigidbody rb;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
        // Rigidbody 속도 → 로컬 방향 기준으로 변환
        Vector3 velocity = rb.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        Vector3 horizontalLocalVelocity = new Vector3(localVelocity.x, 0, localVelocity.z);

        // 속도 크기 = Speed 파라미터
        float speed = horizontalLocalVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        // Shift 키로 달리기 감지
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isRunning", isRunning);

        // 점프 상태 감지
        bool isJumping = !IsGrounded() && Mathf.Abs(rb.velocity.y) > 0.1f;
        animator.SetBool("isJumping", isJumping);
    }

    bool IsGrounded()
    {
        // 땅 체크: 현재 위치에서 아래로 Ray 쏴서 groundLayer에 닿는지 확인
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // 디버깅용: 바닥 체크 Ray 그리기
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
