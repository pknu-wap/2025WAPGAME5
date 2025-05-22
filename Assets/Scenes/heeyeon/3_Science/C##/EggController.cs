using UnityEngine;

public class EggController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool isStopped = false;

    public DistanceMeter distanceMeter;

    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        initialPosition = transform.position;
    }

    public void StartFalling()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ResetEgg()
    {
        rb.isKinematic = true;

        transform.position = new Vector3(420f, 670f, -0.2f);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.isKinematic = false;
    }

    void Update()
    {
        if (!isFalling || isStopped) return;

        float eggBottomY = transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        float lineBottomY = distanceMeter.GetLineBottomY();

        if (eggBottomY <= lineBottomY)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            isStopped = true;
            distanceMeter.CalculateScore(transform.position, true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            isStopped = true;
            distanceMeter.CalculateScore(transform.position, false);
        }
    }
}
