using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NoteGenerator : MonoBehaviour
{
    public GameObject Note_R;
    public GameObject Note_L;
    public GameObject Spectrum;
    public TextMeshProUGUI ReadyText;
    public GameObject StartPanel;
    public GameObject SongPanel;

    public float bpm = 240f;
    private Coroutine currentIsCoroutine;

    private Note scriptNoteR;
    private Note scriptNoteL;

    private AudioSource audioSource;
    private float time_Signatures = 0f;

    private List<bool[]> patternList;
    private int currentPatternIndex = 0;
    private bool isPlayingPattern = false;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        scriptNoteR = Note_R.GetComponent<Note>();
        scriptNoteL = Note_L.GetComponent<Note>();

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 패턴 초기화
        patternList = new List<bool[]>()
        {
            new bool[] { false, true, false, true },         // 1
            new bool[] { true, true, false, false },          // 2
            new bool[] { false, false, false, false },        // 3
            new bool[] { true, true, true, true },            // 4
            new bool[] { false, false, true, true, false },   // 5
            new bool[] { true, false, true, false, true },    // 6
        };
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f && !gameStarted)
        {
            StartPanel.SetActive(false);
            Time.timeScale = 1f;
            gameStarted = true;
            Spectrum.SetActive(true);
            PlayNextPattern();

        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f && gameStarted && currentPatternIndex >= patternList.Count)
        {
            SongPanel.SetActive(false);
            Time.timeScale = 1f;
            

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                currentIsCoroutine = StartCoroutine(PlaySong1());
            }

        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) // ~키
        {
            PlayerPrefs.SetInt("ReturnedFromMusic", 1);
            SceneManager.LoadScene("ClassRoom");
        }
    }

    void PlayNextPattern()
    {
        if (currentPatternIndex < patternList.Count)
        {
            currentIsCoroutine = StartCoroutine(PlayPattern(patternList[currentPatternIndex]));
            currentPatternIndex++;
        }
        else
        {
            Debug.Log("모든 패턴 재생 완료");
            SongPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    IEnumerator PlayPattern(bool[] pattern)
    {
        isPlayingPattern = true;
        float beatDuration = 60f / bpm;

        foreach (bool right in pattern)
        {
            if (right) scriptNoteR.Create_Note();
            else scriptNoteL.Create_Note();
            yield return new WaitForSeconds(beatDuration);
        }

        isPlayingPattern = false;
        currentIsCoroutine = null;

        // 다음 패턴 자동 재생
        yield return new WaitForSeconds(0.5f); // 약간의 텀
        PlayNextPattern();
    }

    IEnumerator PlaySong1()
    {
        bpm = 175f;
        float beat = 60f / bpm;
        float timeUnit = beat / 4f;
        float triplet = beat / 3f;

        time_Signatures = 60 / bpm * 8f;
        yield return new WaitForSeconds(time_Signatures * 8 - 4f);

        ReadyText.gameObject.SetActive(true);
        ReadyText.text = "3"; yield return new WaitForSeconds(1f);
        ReadyText.text = "2"; yield return new WaitForSeconds(1f);
        ReadyText.text = "1"; yield return new WaitForSeconds(1f);
        ReadyText.gameObject.SetActive(false);

        for (int i = 0; i < 32; i++)
        {
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 4);
        }

        yield return new WaitForSeconds(time_Signatures * 16 - 3f);

        ReadyText.gameObject.SetActive(true);
        ReadyText.text = "3"; yield return new WaitForSeconds(1f);
        ReadyText.text = "2"; yield return new WaitForSeconds(1f);
        ReadyText.text = "1"; yield return new WaitForSeconds(1f);
        ReadyText.gameObject.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 16);
        }

        scriptNoteR.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 2);

        for (int i = 0; i < 6; i++)
        {
            scriptNoteL.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        }

        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteL.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);
        scriptNoteR.Create_Note(); yield return new WaitForSeconds(time_Signatures / 8);

        yield return new WaitForSeconds(10f);
        audioSource.Stop();
        Spectrum.SetActive(false);
        Debug.Log(Rhythm_Judgement.Rhythm_Score);

        PlayerPrefs.SetInt("ReturnedFromMusic", 1);
        SceneManager.LoadScene("ClassRoom");
    }
}
