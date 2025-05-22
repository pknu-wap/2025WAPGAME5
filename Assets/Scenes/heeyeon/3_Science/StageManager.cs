using UnityEngine;
using TMPro;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public Sciencemanager Sciencemanager;

    public int currentStage = 1;
    public int maxStage = 4;

    public EggController eggController;
    public EggFollowCamera cameraController;

    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowStageInstruction(currentStage);
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

    // 달걀 멈추고 점수 나올 때 호출 (스페이스바 또는 바닥 닿을 때)
    public void OnStageClear(int score)
    {
        StartCoroutine(ShowScoreThenReset(score));
    }

    IEnumerator ShowScoreThenReset(int score)
    {
        // 3) 2초 대기
        yield return new WaitForSeconds(2f);

        // 5) 초기화
        eggController.ResetEgg();

        // 6) 스테이지 올리기
        currentStage++;
        if (currentStage > maxStage)
        {
            instructionText.text = "모든 스테이지 완료!\n수고하셨습니다.";
            instructionText.gameObject.SetActive(true);
            yield break;
        }

        // 7) 스테이지 안내 다시 보여주기
        ShowStageInstruction(currentStage);
        //다시 시작!
        yield return StartCoroutine(Sciencemanager.StartCountdown());
    }
}
