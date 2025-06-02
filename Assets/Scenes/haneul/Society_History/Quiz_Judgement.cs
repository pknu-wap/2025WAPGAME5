using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz_judgement : MonoBehaviour
{
    [System.Serializable]
    public class Choice
    {
        public Button button;
        public bool isCorrect;
    }

    public List<Choice> choices;

    public History_Timer timer;

    public GameObject CorrectPrefab;
    public GameObject WrongPrefab;

    public Transform spawnParent;

    private Quiz_Random quizRandom;

    void Start()
    {
        quizRandom = FindObjectOfType<Quiz_Random>();

        foreach (var choice in choices)
        {
            var capturedChoice = choice; // 로컬 변수 복사
            choice.button.onClick.AddListener(() => OnChoiceSelected(capturedChoice));
        }
    }

    void OnChoiceSelected(Choice choice)
    {
        if (choice.isCorrect)
        {
            Instantiate(CorrectPrefab, spawnParent);
            Debug.Log("정답!");

            int currentCorrect = PlayerPrefs.GetInt("CorrectHistory", 0);
            PlayerPrefs.SetInt("CorrectHistory", currentCorrect + 1);
        }
        else
        {
            Instantiate(WrongPrefab, spawnParent);
            Debug.Log("오답!");
            timer.WrongAnswer();
        }

        quizRandom.ShowNextQuiz();
    }
}
