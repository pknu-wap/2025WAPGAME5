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

    void Start()
    {
        foreach (var choice in choices)
        {
            choice.button.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    void OnChoiceSelected(Choice choice)
    {
        if (choice.isCorrect)
        {
            Instantiate(CorrectPrefab, spawnParent);
            Debug.Log("정답!");
            History_Correct();
        }
        else
        {
            Instantiate(WrongPrefab, spawnParent);
            Debug.Log("오답!");
            timer.WrongAnswer();
        }
    }

    void History_Correct()
    {
    
    }

}
