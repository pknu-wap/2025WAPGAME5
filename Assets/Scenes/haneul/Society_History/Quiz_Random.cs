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

                int correctCount = PlayerPrefs.GetInt("Correct_History", 0);
                float elapsed = timer.timeElapsed;

                // 정답 점수 (70점 만점)
                float scorePerQuestion = 70f / 12f;
                float correctScore = correctCount * scorePerQuestion;

                // 시간 점수 (30점 만점)
                float timeScore = 0f;
                if (elapsed <= 60f)
                    timeScore = 30f;
                else if (elapsed <= 90f)
                    timeScore = 20f;
                else if (elapsed <= 120f)
                    timeScore = 10f;
                else
                    timeScore = 0f;

                int finalScore = Mathf.RoundToInt(correctScore + timeScore);

                Debug.Log($"{elapsed:F0}초 걸림");
                Debug.Log("정답 수: " + correctCount);
                Debug.Log("정답 점수: " + correctScore + ", 시간 점수: " + timeScore);
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
