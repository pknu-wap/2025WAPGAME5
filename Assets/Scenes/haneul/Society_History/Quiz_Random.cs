using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Quiz_Random : MonoBehaviour
{
    public List<GameObject> allQuizObjects;
    public TextMeshProUGUI quizProgressText;

    private List<GameObject> selectedQuizzes = new List<GameObject>();
    private int currentQuizIndex = 0;

    public GameObject Timer;
    private History_Timer timer;

    void Start()
    {
        PlayerPrefs.SetInt("CorrectHistory", 0);
        PlayerPrefs.Save();

        timer = Timer.GetComponent<History_Timer>();
        Timer.SetActive(true);
        Shuffle(allQuizObjects);
        selectedQuizzes = allQuizObjects.GetRange(0, 12);

        foreach (var quiz in allQuizObjects)
            quiz.SetActive(false);

        currentQuizIndex = 0;
        ShowCurrentQuiz();
    }

    void ShowCurrentQuiz()
    {
        if (currentQuizIndex < selectedQuizzes.Count)
        {
            selectedQuizzes[currentQuizIndex].SetActive(true);
            quizProgressText.text = $"{currentQuizIndex + 1}/12";
        }
        else
        {
            quizProgressText.text = "";
            Debug.Log("퀴즈 종료");
        }
    }

    public void ShowNextQuiz()
    {
        if (currentQuizIndex < selectedQuizzes.Count)
        {
            selectedQuizzes[currentQuizIndex].SetActive(false);
            currentQuizIndex++;

            if (currentQuizIndex < selectedQuizzes.Count)
            {
                selectedQuizzes[currentQuizIndex].SetActive(true);
                quizProgressText.text = $"{currentQuizIndex + 1}/12";
            }
            else
            {
                quizProgressText.text = "";
                timer.TimeStop();

                int correctCount = PlayerPrefs.GetInt("CorrectHistory", 0);

                // 정답 점수 (70점 만점)
                float scorePerQuestion = 70f / 12f;
                float correctScore = correctCount * scorePerQuestion;

                // 시간 점수 (30점 만점)
                float elapsed = timer.timeElapsed;

                // 시간 점수: 30초까지 만점, 60초 이상이면 0점
                float maxScoreTime = 30f;
                float zeroScoreTime = 60f;
                float maxTimeScore = 30f;

                float timeRatio = (elapsed - maxScoreTime) / (zeroScoreTime - maxScoreTime);
                float timeScore = Mathf.Clamp((1f - timeRatio) * maxTimeScore, 0f, maxTimeScore);

                int finalScore = Mathf.RoundToInt(correctScore + timeScore);

                Debug.Log($"{elapsed:F0}초 걸림");
        
                Debug.Log("역사 점수 저장됨: " + finalScore);

                PlayerPrefs.SetInt("Score_History", finalScore);
                PlayerPrefs.SetInt("ReturnedFromHistory", 1);
                SceneManager.LoadScene("ClassRoom");
            }
        }
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            GameObject temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
