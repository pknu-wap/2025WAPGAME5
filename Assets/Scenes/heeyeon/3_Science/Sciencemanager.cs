using System.Collections;
using UnityEngine;
using TMPro;

public class Sciencemanager : MonoBehaviour
{
    public static bool gameStarted = false;

    public TextMeshProUGUI countdownText;
    public EggController eggController;

    void Start()
    {
        gameStarted = false;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "START!";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        gameStarted = true;
        eggController.StartFalling();
    }
}
