using UnityEngine;
using UnityEngine.UI;

public class FinalGradeCalculator : MonoBehaviour
{
    public Image stampImage;
    public Image gradeAPlus, gradeA, gradeB, gradeC, gradeD, gradeF;

    public Image O;
    public Image X;

    void Start()
    {
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

        if (badMoodCount >= 7) averageScore -= 10f;
        if (isLate == 1) averageScore -= 10f;

        if (averageScore >= 95) {gradeAPlus.gameObject.SetActive(true);}
        else if (averageScore >= 90) {gradeA.gameObject.SetActive(true);}
        else if (averageScore >= 80) {gradeB.gameObject.SetActive(true);}
        else if (averageScore >= 70) {gradeC.gameObject.SetActive(true);}
        else if (averageScore >= 60) {gradeD.gameObject.SetActive(true);}
        else {gradeF.gameObject.SetActive(true);}

        if (isLate == 1) { O.gameObject.SetActive(true);}
        else if (isLate == 0) { X.gameObject.SetActive(true); }

    }
}
