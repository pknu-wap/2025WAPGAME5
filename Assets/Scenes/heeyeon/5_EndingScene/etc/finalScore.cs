using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FinalGradeCalculator : MonoBehaviour
{
    public Image stampImage;
    public Image gradeAPlus, gradeA, gradeB, gradeC, gradeD, gradeF;
    public Image O;
    public Image X;
    public TMP_Text resultText1;
    public TMP_Text resultText2;
    public TMP_Text resultText3;
    public TMP_Text resultText4;
    public TMP_Text resultText5;

    void Start()
    {
        StartCoroutine(ShowFinalResultSequence());
    }

    IEnumerator ShowFinalResultSequence()
    {
        yield return new WaitForSeconds(18f);

        int scoreKor = PlayerPrefs.GetInt("Score_Korean", 0);
        int scoreSci = PlayerPrefs.GetInt("Score_Science", 0);
        int scoreProg = PlayerPrefs.GetInt("Score_Programming", 0);
        int scoreHist = PlayerPrefs.GetInt("Score_History", 0);
        int scoreMath = PlayerPrefs.GetInt("Score_Math", 0);
        int scoreMusic = PlayerPrefs.GetInt("Score_Music", 0);
        int totalScore = scoreKor + scoreSci + scoreProg + scoreHist + scoreMath + scoreMusic;

        float averageScore = totalScore / 6f;

        int badMoodCount = PlayerPrefs.GetInt("MoodCount_Worst", 0);  // 기분 가장 나쁜 표정 등장 횟수
        int isLate = PlayerPrefs.GetInt("IsLate", 0); // 1이면 지각

        float deductedByMood = badMoodCount >= 7 ? 10f : 0f;
        float deductedByLate = isLate == 1 ? 5f : 0f;
        float finalAverage = averageScore - deductedByMood - deductedByLate;

        // Step 1: 지각 여부 이미지 표시
        if (isLate == 1)
            O.gameObject.SetActive(true);
        else
            X.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        // Step 2: 표정 관련 이미지 표시 (아직 미구현)
        // TODO: Show mood-based image here

        // Step 3: 텍스트 표시
        resultText1.text = $"6과목 평균: {averageScore:F1}점";
        yield return new WaitForSeconds(1f);

        if (deductedByLate > 0)
            resultText2.text = "<color=red>- 지각 5점</color>";

        else
            resultText2.text = "<color=green>지각 감점 없음!</color>";
        yield return new WaitForSeconds(1f);

        if (deductedByMood > 0)
            resultText3.text = $"<color=red>- 나쁜 기분({badMoodCount}회) 10점</color>";
        else
            resultText3.text = "<color=green>기분 감점 없음!</color>";
        yield return new WaitForSeconds(1f);

        resultText4.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        resultText5.text = $"최종 점수: {finalAverage:F1}점";
        resultText5.color = Color.red;

        yield return new WaitForSeconds(1.5f);

        // Step 4: 학점 도장 표시
        if (finalAverage >= 95) gradeAPlus.gameObject.SetActive(true);
        else if (finalAverage >= 90) gradeA.gameObject.SetActive(true);
        else if (finalAverage >= 80) gradeB.gameObject.SetActive(true);
        else if (finalAverage >= 70) gradeC.gameObject.SetActive(true);
        else if (finalAverage >= 60) gradeD.gameObject.SetActive(true);
        else gradeF.gameObject.SetActive(true);
    }
}
