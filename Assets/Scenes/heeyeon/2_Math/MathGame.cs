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
    public TextMeshProUGUI progressText;

    private float startTime;
    private bool gameStarted = false;
    private int currentQuestionIndex = 0;
    private int totalQuestions = 10;

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
    List<Question> allQuestions = new List<Question>
    {
        new Question("14 × 14 = ?", 196),
        new Question("7(6 + 8) = ?", 98),
        new Question("7 × 2 = ?", 14),
        new Question("9 ÷ 3 = ?", 3),
        new Question("10 + 15 = ?", 25),
        new Question("(18 + 24)÷(6-3) = ?", 14),
        new Question("3 × 15 = ?", 45),
        new Question("20 ÷ 4 ÷ 5 = ?", 1),
        new Question("25 + 25 ÷ 5 = ?", 30),
        new Question("81 ÷ 9 = ?", 9),
        new Question("11 × 3 = ?", 33),
        new Question("18 - 7 - 2 = ?", 9),
        new Question("5 + 7 × 2 = ?", 19),
        new Question("16 ÷ 2 ÷ 2 = ?", 8),
        new Question("4 × 6 ÷ 12 = ?", 2),
        new Question("13 + 8 = ?", 21),
        new Question("35 - 9 = ?", 26)
    };

    ShuffleList(allQuestions);

    int questionCount = 10;
    for (int i = 0; i < questionCount && i < allQuestions.Count; i++)
    {
        questions.Add(allQuestions[i]);
    }
}

void ShuffleList(List<Question> list)
{
    for (int i = 0; i < list.Count; i++)
    {
        int randomIndex = Random.Range(i, list.Count);
        (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
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
    progressText.text = $"{currentQuestionIndex + 1}/{totalQuestions}";
    
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
