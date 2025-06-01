using UnityEngine;
using UnityEngine.UI;

public class FinalGradeCalculator : MonoBehaviour
{
    public Image stampImage;
    public Sprite gradeAPlus, gradeA, gradeB, gradeC, gradeD, gradeF;

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

        string grade = "";
        Sprite selectedStamp = null;

        if (averageScore >= 95) { grade = "A+"; selectedStamp = gradeAPlus; }
        else if (averageScore >= 90) { grade = "A"; selectedStamp = gradeA; }
        else if (averageScore >= 80) { grade = "B"; selectedStamp = gradeB; }
        else if (averageScore >= 70) { grade = "C"; selectedStamp = gradeC; }
        else if (averageScore >= 60) { grade = "D"; selectedStamp = gradeD; }
        else { grade = "F"; selectedStamp = gradeF; }

        Debug.Log($"학점은: {grade}");
        stampImage.sprite = selectedStamp;
        stampImage.gameObject.SetActive(true);
    }
}
