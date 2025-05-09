using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player22 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isCrash = false;

    private bool isPlaying = false;
    private bool isPaused = false;
    private bool isRotating = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isPlaying || isPaused || isRotating) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC") && !isRotating && !isCrash)
        {
            StartCoroutine(RotateAroundPlayer());
        }
    }

    IEnumerator RotateAroundPlayer()
    {
        isPaused = true;
        isRotating = true;
        isCrash = true;

        float duration = 2f;
        float elapsed = 0f;
        float startY = transform.eulerAngles.y;
        GameManager.currentEmotion = 5;

        while (elapsed < duration)
        {
            float angle = Mathf.Lerp(0f, 360f, elapsed / duration);
            transform.rotation = Quaternion.Euler(0f, startY + angle, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, startY + 360f, 0f);
        isPaused = false;
        isRotating = false;
        yield return new WaitForSeconds(2f);
        isCrash = false;
        GameManager.currentEmotion = 0;
    }

    public void SetPlaying(bool playing)
    {
        isPlaying = playing;
        isPaused = false;
    }
}
