using UnityEngine;

public class EggController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool isStopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void StartFalling()
    {
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        if (isFalling && !isStopped && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            isStopped = true;

            FindObjectOfType<DistanceMeter>().CalculateScore(transform.position);
        }
    }
}
