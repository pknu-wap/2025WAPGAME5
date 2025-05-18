using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteGenerator : MonoBehaviour
{
    public GameObject Note_R;
    public GameObject Note_L;
    public GameObject Spectrum;
    public TextMeshProUGUI ReadyText;
    public GameObject StartPanel;

    public float bpm = 240f;
    private Coroutine currentIsCoroutine;

    private Note scriptNoteR;
    private Note scriptNoteL;

    private AudioSource audioSource;
    private float time_Signatures = 0f;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale==0f)
        {
            StartPanel.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            scriptNoteL.Create_Note();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            scriptNoteR.Create_Note();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Spectrum.SetActive(true);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                currentIsCoroutine=StartCoroutine(PlaySong1());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayPatternSafe(new bool[] { false, true, false, true });
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayPatternSafe(new bool[] { true, true, false, false });
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlayPatternSafe(new bool[] { false, false, false, false });
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlayPatternSafe(new bool[] { true, true, true, true });
        if (Input.GetKeyDown(KeyCode.Alpha5)) PlayPatternSafe(new bool[] { false, false, true, true, false });
        if (Input.GetKeyDown(KeyCode.Alpha6)) PlayPatternSafe(new bool[] { true, false, true, false, true });
        if (Input.GetKeyDown(KeyCode.Alpha7)) PlayPatternSafe(new bool[] { false, true, true, false, true });
        if (Input.GetKeyDown(KeyCode.Alpha8)) PlayPatternSafe(new bool[] { true, false, false, true, false });
        if (Input.GetKeyDown(KeyCode.Alpha9)) PlayPatternSafe(new bool[] { false, false, true, false, true });
        if (Input.GetKeyDown(KeyCode.Alpha0)) PlayPatternSafe(new bool[] { true, true, false, true, false });
    }

    void PlayPatternSafe(bool[] pattern)
    {
        if (currentIsCoroutine != null)
        {
            StopCoroutine(currentIsCoroutine);
        }
        currentIsCoroutine = StartCoroutine(PlayPattern(pattern));
    }

    IEnumerator PlayPattern(bool[] pattern)
    {
        float beatDuration = 60f / bpm;

        foreach (bool right in pattern)
        {
            if (right)
            {
                scriptNoteR.Create_Note();
            }
            else
            {
                scriptNoteL.Create_Note();
            }
            yield return new WaitForSeconds(beatDuration);
        }

        currentIsCoroutine = null;
    }
    IEnumerator PlaySong1()
    {
        bpm = 175f;
        float beat = 60f / bpm;             // 한 박자 (quarter note)
        float timeUnit = beat / 4f;         // 16분음표 단위
        float triplet = beat / 3f;          // 셋잇단음표 단위

        time_Signatures = 60 / bpm * 8f;
        yield return new WaitForSeconds(time_Signatures*8-4f);
        
        ReadyText.gameObject.SetActive(true);

        ReadyText.text = "3";
        yield return new WaitForSeconds(1f);

        ReadyText.text = "2";
        yield return new WaitForSeconds(1f);

        ReadyText.text = "1";
        yield return new WaitForSeconds(1f);

        ReadyText.gameObject.SetActive(false);

        for (int i=0;i < 32; i++)
        {
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 4);
        }
        yield return new WaitForSeconds(time_Signatures * 16-3f);

        ReadyText.gameObject.SetActive(true);

        ReadyText.text = "3";
        yield return new WaitForSeconds(1f);

        ReadyText.text = "2";
        yield return new WaitForSeconds(1f);

        ReadyText.text = "1";
        yield return new WaitForSeconds(1f);

        ReadyText.gameObject.SetActive(false);

        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 16);
        scriptNoteR.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 2);

        for(int i = 0; i < 7; i++)
        {
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteL.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
            scriptNoteR.Create_Note();
            yield return new WaitForSeconds(time_Signatures / 8);
        }
        yield return new WaitForSeconds(2f- time_Signatures / 8);
        audioSource.Stop();
        Spectrum.SetActive(false);
        Debug.Log(Rhythm_Judgement.Rhythm_Score);
    }

}
