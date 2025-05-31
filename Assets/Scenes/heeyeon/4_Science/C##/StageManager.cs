using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject fakeLine;
    public GameObject handBlocker;
    public GameObject blackFlashImage;
    private bool hasFlippedScreen = false;

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
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);

        instructionText.text = $"-- 스테이지 {stage} --\n<color=#FF0000>빨간 선</color>에 최대한 가깝게 닿도록 하세요!";
        instructionText.gameObject.SetActive(true);
        Invoke(nameof(HideInstruction), 3f);

        if (fakeLine != null)
        {
            fakeLine.SetActive(stage == 2); 
        }

        if (handBlocker != null)
        {
            handBlocker.SetActive(stage == 3);
        }

        if (stage == 4 && !hasFlippedScreen)
        {
            StartCoroutine(PlayCombinedEffect());
            hasFlippedScreen = true;
        }
    }

    IEnumerator PlayCombinedEffect()
    {
        yield return new WaitForSeconds(8f);

        blackFlashImage.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        blackFlashImage.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        blackFlashImage.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        blackFlashImage.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        blackFlashImage.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        blackFlashImage.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);

        yield return new WaitForSeconds(1.0f);
        Camera.main.transform.rotation = Quaternion.identity;
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
        countdownText.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(2f);

        scoreText.gameObject.SetActive(false);

        eggController.ResetEgg();
        eggController.StopEgg();
        distanceMeter.ClearText();

        currentStage++;
        if (currentStage > maxStage)
        {
            float averageScore = distanceMeter.GetAverageScore();
            instructionText.text = $"모든 스테이지 완료!\n\n평균 점수: {averageScore:F1}";
            instructionText.gameObject.SetActive(true);

            PlayerPrefs.SetInt("ReturnedFromScience", 1);
            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("ClassRoom");
            yield break;
        }

        instructionText.gameObject.SetActive(true);
        ShowStageInstruction(currentStage); //스테이지 안내 뜨고

        yield return StartCoroutine(StartCountdown());
    }
}
