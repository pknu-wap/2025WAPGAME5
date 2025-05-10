using UnityEngine;
using TMPro;
using System.Collections;

public class EggFall : MonoBehaviour
{
    public Rigidbody eggRb;
    public Transform ground;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI countdown;
    public Camera mainCamera;

    private bool hasStopped = false;
    private bool hasFailed = false;
    private bool gameStarted = false;

    void Start()
    {
        resultText.gameObject.SetActive(false);
        countdown.text = "";
        eggRb.isKinematic = true;
        StartCoroutine(CountdownAndDrop());
    }

    IEnumerator CountdownAndDrop()
    {
        countdown.text = "3"; yield return new WaitForSeconds(1f);
        countdown.text = "2"; yield return new WaitForSeconds(1f);
        countdown.text = "1"; yield return new WaitForSeconds(1f);
        countdown.text = "³«ÇÏ!"; yield return new WaitForSeconds(0.5f);

        countdown.gameObject.SetActive(false);
        eggRb.isKinematic = false;

        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted && !hasStopped && !hasFailed && Input.GetKeyDown(KeyCode.Space))
        {
            StopEgg();
        }
    }

    void StopEgg()
    {
        eggRb.velocity = Vector3.zero;
        eggRb.isKinematic = true;
        hasStopped = true;

        float distance = Vector3.Distance(eggRb.position, ground.position);
        StartCoroutine(ShowResult($"{distance * 100f:F2}cm", false));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!gameStarted || hasStopped || hasFailed) return;

        if (collision.collider.CompareTag("Ground"))
        {
            hasFailed = true;
            eggRb.isKinematic = true;
            StartCoroutine(ShowResult("Å»¶ô!", true));
        }
    }

    IEnumerator ShowResult(string message, bool isFail)
    {
        mainCamera.transform.position = new Vector3(
            eggRb.position.x,
            ground.position.y + 0.1f,
            eggRb.position.z - 0.4f
        );
        mainCamera.transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        resultText.text = message;
        resultText.gameObject.SetActive(true);

        if (isFail)
        {
            yield return new WaitForSeconds(2f);
            resultText.gameObject.SetActive(false);
            // ´ÙÀ½ ´Ü°è
        }
    }
}
