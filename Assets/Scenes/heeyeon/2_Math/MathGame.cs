using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class MathGame : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;

    private float startTime;
    private bool gameStarted = false;
    private int currentQuestionIndex = 0;

    private List<Question> questions = new List<Question>();

    void Start()
    {
        answerInput.caretWidth = 0; 
        answerInput.customCaretColor = true;
        answerInput.caretColor = new Color(0, 0, 0, 0);
        answerInput.selectionColor = new Color(0, 0, 0, 0);

        CreateQuestions();

        questionText.gameObject.SetActive(false);
        answerInput.gameObject.SetActive(false);
        answerInput.text = "";

        StartCoroutine(CountdownAndStart());
    }

    void CreateQuestions()
    {
        questions.Add(new Question("2 + 3 = ?", 5));
        questions.Add(new Question("6 - 1 = ?", 5));
        questions.Add(new Question("7 × 2 = ?", 14));
        questions.Add(new Question("9 ÷ 3 = ?", 3));
        questions.Add(new Question("10 + 15 = ?", 25));
        questions.Add(new Question("12 - 4 = ?", 8));
        questions.Add(new Question("3 × 5 = ?", 15));
        questions.Add(new Question("20 ÷ 4 = ?", 5));

        ShuffleQuestions();
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            (questions[i], questions[randomIndex]) = (questions[randomIndex], questions[i]);
        }
    }

    IEnumerator CountdownAndStart()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "시작!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        startTime = Time.time;
        gameStarted = true;

        questionText.gameObject.SetActive(true);
        answerInput.gameObject.SetActive(true);
        ShowNextQuestion();
    }

    void Update()
    {
        if (!gameStarted) return;

        float elapsed = Time.time - startTime;
        timerText.text = elapsed.ToString("F2") + "초";

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitAnswer();
        }
    }

    void ShowNextQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            EndGame();
            return;
        }

        var q = questions[currentQuestionIndex];
        questionText.text = q.question;
        answerInput.text = "";
        answerInput.Select(); 
        answerInput.ActivateInputField(); 
    }

    void SubmitAnswer()
    {
        if (int.TryParse(answerInput.text, out int userAnswer))
        {
            if (userAnswer == questions[currentQuestionIndex].answer)
            {
                currentQuestionIndex++;
                ShowNextQuestion();
            }
            else
            {
                Debug.Log("오답!");
                answerInput.text = "";
                answerInput.Select();
                answerInput.ActivateInputField();
            }
        }
        else
        {
            Debug.Log("숫자를 입력하세요");
            answerInput.text = "";
            answerInput.Select();
            answerInput.ActivateInputField();
        }
    }

    void EndGame()
    {
        float totalTime = Time.time - startTime;
        Debug.Log("총 풀이 시간: " + totalTime + "초");
        PlayerPrefs.SetFloat("MathTime", totalTime);
        SceneManager.LoadScene("heeyeon");
    }

    class Question
    {
        public string question;
        public int answer;

        public Question(string q, int a)
        {
            question = q;
            answer = a;
        }
    }
}
