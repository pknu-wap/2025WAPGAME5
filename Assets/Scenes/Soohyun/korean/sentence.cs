using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sentence : MonoBehaviour
{
    public TextMeshProUGUI sentence1;
    public TextMeshProUGUI sentence2;
    public TextMeshProUGUI timer;
    public TMP_InputField inputField;

    private List<string> sentenceList = new List<string>()
    {
        "동해물과 백두산이 마르고 닳도록",
        "하느님이 보우하사 우리나라 만세",
        "무궁화 삼천리 화려 강산",
        "대한 사람 대한으로 길이 보전하세"
    };
    private int currentIndex = 0;
    private float startTime;
    private bool isGameRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        isGameRunning = true;
        ShowCurrentSentence();
        inputField.onSubmit.AddListener(OnInputSubmit);
        inputField.onValueChanged.AddListener(OnInputChanged);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowCurrentSentence()
    {
        if (currentIndex < sentenceList.Count)
        {
            sentence1.text = sentenceList[currentIndex];
            inputField.text = "";
            inputField.ActivateInputField();
        }
        else
        {
            sentence1.text = " 완료!";
            inputField.gameObject.SetActive(false);
        }
    }
    void OnInputSubmit(string userInput)
    {
        if (userInput.Trim() == sentenceList[currentIndex])
        {
            currentIndex++;
            ShowCurrentSentence();
        }
        else
        {
            Debug.Log("틀림!");
        }
    }

    void OnInputChanged(string input)
    {
        string correct = sentenceList[currentIndex];
        string highlighted = "";

            for (int i = 0; i < correct.Length; i++)
            {
                if (i < input.Length)
                {
                    if (input[i] == correct[i])
                        highlighted += correct[i];
                    else
                        highlighted += $"<color=red>{correct[i]}</color>";
                }
                else
                {
                    highlighted += correct[i];
                }
            }
            sentence1.text = highlighted;
        }
    void OnDestroy()
    {
        inputField.onSubmit.RemoveListener(OnInputSubmit);
        inputField.onValueChanged.RemoveListener(OnInputChanged);
    }
}
