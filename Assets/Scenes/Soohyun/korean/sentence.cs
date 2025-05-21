using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sentence : MonoBehaviour
{
    public TextMeshProUGUI sentence1;
    public TextMeshProUGUI sentence2;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI counter;
    public TMP_InputField inputField;


    private List<List<string>> sentenceList = new List<List<string>>()
    {
        new List<string>(){ "동해물과 백두산이 마르고 닳도록",
        "하느님이 보우하사 우리나라 만세",
        "무궁화 삼천리 화려 강산",
        "대한 사람 대한으로 길이 보전하세"
        },

        new List<string>(){"남산 위에 저 소나무 철갑을 두른 듯",
        "바람 서리 불변함은 우리 기상일세",
        "무궁화 삼천리 화려 강산",
        "대한 사람 대한으로 길이 보전하세"
        },

        new List<string>(){"가을 하늘 공활한데 높고 구름 없이",
        "밝은 달은 우리 가슴 일편단심일세",
        "무궁화 삼천리 화려 강산",
        "대한 사람 대한으로 길이 보전하세"
        },

        new List<string>(){"이 기상과 이 맘으로 충성을 다하여",
        "괴로우나 즐거우나 나라 사랑하세",
        "무궁화 삼천리 화려 강산",
        "대한 사람 대한으로 길이 보전하세"
        }

    };
    private int randomindex ;
    private int randomindexx;
    private int playTime=1;
    private int currentIndex = 0;
    private float startTime;
    private bool isGameRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        isGameRunning = true; 
        randomindex = Random.Range(0, 4);
        ShowCurrentSentence();
        inputField.onSubmit.AddListener(OnInputSubmit);
        inputField.onValueChanged.AddListener(OnInputChanged);

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            counter.text = $"{playTime}/2";
            float elapsed = Time.time - startTime;
            timer.text = $"시간: {elapsed:F1}초";
        }
    }
    void ShowCurrentSentence()
    {
        //반복 횟수 2번 이하
        if (playTime <= 2)
        {
            sentence1.text = sentenceList[randomindex][currentIndex];
            if (currentIndex < 3)
            {
                sentence2.text = sentenceList[randomindex][currentIndex + 1];
            }
            // 3번째 문장일때
            else
            {
                if (playTime == 2)
                {
                    sentence2.gameObject.SetActive(false);
                }
                else
                {
                    do
                    {
                        randomindexx = Random.Range(0, 4);
                    } while (randomindexx == randomindex);
                    Debug.Log(randomindexx);
                    sentence2.text = sentenceList[randomindexx][0];
                }
            }
            inputField.text = "";
            inputField.ActivateInputField();
        }
        else
        {
            sentence1.text = " 완료!";
            inputField.gameObject.SetActive(false);
            Debug.Log(timer.text);
            isGameRunning = false;
        }
    }
    void OnInputSubmit(string userInput)
    {
        if (userInput.Trim() == sentenceList[randomindex][currentIndex])
        {
            if (currentIndex >= 3)
            {
                randomindex= randomindexx;
                currentIndex = 0;
                ++playTime;
                Debug.Log(playTime);
                ShowCurrentSentence();
            }
            else
            {
                currentIndex++;
                ShowCurrentSentence();
            }
        }
        else
        {
            Debug.Log("틀림!");
        }
        inputField.ActivateInputField();
    }

    void OnInputChanged(string input)
    {
        string correct = sentenceList[randomindex][currentIndex];
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
