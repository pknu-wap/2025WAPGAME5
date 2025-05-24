using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class History_Quiz_Start : MonoBehaviour
{
    public GameObject targetObject;
    public TextMeshProUGUI countdownText;
    public Image progressCircle;

    private float countdownTime = 3f;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float timer = countdownTime;

        while (timer > 0f)
        {
            int displayTime = Mathf.CeilToInt(timer);
            countdownText.text = displayTime.ToString();

            progressCircle.fillAmount = 1 - (timer / countdownTime);

            yield return null;
            timer -= Time.deltaTime;
        }

        countdownText.text = "START";
        progressCircle.fillAmount = 1f;

        yield return new WaitForSeconds(0.5f);


        if (targetObject != null)
            targetObject.SetActive(true);


        gameObject.SetActive(false);
    }
}
