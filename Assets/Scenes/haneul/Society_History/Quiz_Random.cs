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

    void Start()
    {
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
            Debug.Log("ÄûÁî Á¾·á");
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
                quizProgressText.text = $"{currentQuizIndex + 1}/8";
            }
            else
            {
                quizProgressText.text = "";
                Debug.Log("ÄûÁî Á¾·á");

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
