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
    public GameObject moodNormalImage;
    public GameObject moodHappyImage;
    public GameObject moodAngryImage;

    public AudioSource audioSource;
    public AudioClip stampSound;
    public AudioClip textAppearSound;

    void Start()
    {
        StartCoroutine(ShowFinalResultSequence());
    }

    private void PlayTextSoundShort()
    {
        if (audioSource && stampSound)
        {
            StartCoroutine(PlayShortClip());
        }
    }

    private IEnumerator PlayShortClip()
    {
        audioSource.clip = stampSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f); // 0.5초만 재생 
        audioSource.Stop();
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

        int angryCount = PlayerPrefs.GetInt("Face_angry_Count", 0);
        int happyCount = PlayerPrefs.GetInt("Face_Happy_Count", 0);
        int moodScoreRaw = happyCount - angryCount;

        int totalScore = scoreKor + scoreSci + scoreProg + scoreHist + scoreMath + scoreMusic;

        float averageScore = totalScore / 6f;

        int isLate = PlayerPrefs.GetInt("IsLate", 0); // 1이면 지각

        float deductedByLate = isLate == 1 ? 5f : 0f;

        // Step 1: 지각 여부 이미지 표시
        if (isLate == 1)
        {
            O.gameObject.SetActive(true);
            audioSource.PlayOneShot(textAppearSound);
        }
            
        else
        {
            X.gameObject.SetActive(true);
            audioSource.PlayOneShot(textAppearSound);
        }
        yield return new WaitForSeconds(1f);

        // Step 2: 표정

        if (moodScoreRaw > 0) // 좋음
        {
            moodHappyImage.SetActive(true);
            audioSource.PlayOneShot(textAppearSound);
        }
        else if (moodScoreRaw < 0) // 나쁨
        {
            moodAngryImage.SetActive(true);
            audioSource.PlayOneShot(textAppearSound);
        }
        else // 보통
        {
            moodNormalImage.SetActive(true);
            audioSource.PlayOneShot(textAppearSound);
        }

        // 감정 점수 반영
        float moodScoreDelta = moodScoreRaw;  // happy - angry (음수면 감점)
        float finalAverage = averageScore - deductedByLate + moodScoreDelta;

        yield return new WaitForSeconds(1f);

        if (happyCount > angryCount)
        {
            moodScoreDelta = happyCount * 1f;
        }
        else if (angryCount > happyCount)
        {
            moodScoreDelta = angryCount * -1f;
        }
            
        // Step 3: 텍스트 표시
        resultText1.text = $"6과목 평균: {averageScore:F1}점";
        audioSource.PlayOneShot(textAppearSound);
        yield return new WaitForSeconds(1f);

        if (deductedByLate > 0)
        {
            resultText2.text = $"<color=red>- 지각 5점</color>";
            audioSource.PlayOneShot(textAppearSound);
        }
        else
        {
            resultText2.text = "<color=#808080>  지각 감점 없음!</color>";
            audioSource.PlayOneShot(textAppearSound);
        }
            
        yield return new WaitForSeconds(1f);

        if (moodScoreDelta > 0)
        {
            resultText3.text = $"<color=green>+ 좋은 기분 {happyCount}점</color>";
            audioSource.PlayOneShot(textAppearSound);
        }
        else if (moodScoreDelta < 0)
        {
            resultText3.text = $"<color=red>- 나쁜 기분 {angryCount}점</color>";
            audioSource.PlayOneShot(textAppearSound);
        }
        else
        {
            resultText3.text = "<color=#808080>  기분 감점 없음!</color>";
            audioSource.PlayOneShot(textAppearSound);
        }

        yield return new WaitForSeconds(1f);

        resultText4.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        resultText5.text = $"최종 점수: {finalAverage:F1}점";
        audioSource.PlayOneShot(textAppearSound);
        resultText5.color = Color.red;

        yield return new WaitForSeconds(1.5f);

        // Step 4: 학점 도장 표시
        if (finalAverage >= 90)
        {
            gradeAPlus.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
        else if (finalAverage >= 80)
        {
            gradeA.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
        else if (finalAverage >= 70)
        {
            gradeB.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
        else if (finalAverage >= 60)
        {
            gradeC.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
        else if (finalAverage >= 50)
        {
            gradeD.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
        else
        {
            gradeF.gameObject.SetActive(true);
            PlayTextSoundShort();
        }
    }
}
