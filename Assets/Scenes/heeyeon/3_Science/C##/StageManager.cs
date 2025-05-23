using UnityEngine;
using TMPro;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public DistanceMeter distanceMeter;

    public int currentStage = 1;
    public int maxStage = 4;

    public EggController eggController;
    public EggFollowCamera cameraController;

    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;

    private bool gameStarted = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowStageInstruction(currentStage);
        StartCoroutine(StartCountdown());
    }

    void ShowStageInstruction(int stage)
    {
        instructionText.text = $"-- 스테이지 {stage} --\n빨간 선에 최대한 가깝게 닿도록 하세요!";
        instructionText.gameObject.SetActive(true);
        Invoke(nameof(HideInstruction), 3f);
    }

    void HideInstruction()
    {
        instructionText.gameObject.SetActive(false);
    }

    public void OnStageClear(float score)
    {
        StartCoroutine(ShowScoreThenReset(score));
    }
    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public IEnumerator StartCountdown()
    {
        instructionText.gameObject.SetActive(true);
        gameStarted = false;
        yield return new WaitForSeconds(3f);

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

    public IEnumerator ShowScoreThenReset(float score)
    {
        Debug.Log("Start Score Reset Coroutine");

        yield return new WaitForSeconds(2f);
        Debug.Log("Wait Done");

        scoreText.gameObject.SetActive(false);

        eggController.ResetEgg();
        eggController.StopEgg();
        Debug.Log("Egg Reset");

        distanceMeter.ClearText();

        currentStage++;
        if (currentStage > maxStage)
        {
            Debug.Log("All stages complete!");
            instructionText.text = "모든 스테이지 완료!\n수고하셨습니다.";
            instructionText.gameObject.SetActive(true);
            yield break;
        }

        ShowStageInstruction(currentStage);
        Debug.Log("Show new stage instruction");

        yield return StartCoroutine(StartCountdown());
        Debug.Log("Countdown complete");
        instructionText.gameObject.SetActive(true);
    }
}
