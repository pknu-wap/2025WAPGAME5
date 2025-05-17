using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

    public class Sciencemanager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public Camera cam;
    public float zoomStartSize = 10f;
    public float zoomEndSize = 5f;
    public float zoomSpeed = 1f;
    public EggController egg;

    void Start()
    {
        cam.orthographicSize = zoomStartSize;
        StartCoroutine(CountdownAndStart());
    }

    IEnumerator CountdownAndStart()
    {
        string[] countTexts = { "3", "2", "1" };
        foreach (string text in countTexts)
        {
            countdownText.text = text;
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "START!";
        yield return new WaitForSeconds(0.5f); 

        countdownText.gameObject.SetActive(false);

        StartCoroutine(ZoomIn());
        egg.StartFalling();
    }

    IEnumerator ZoomIn()
    {
        while (cam.orthographicSize > zoomEndSize)
        {
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
            yield return null;
        }
    }
}
