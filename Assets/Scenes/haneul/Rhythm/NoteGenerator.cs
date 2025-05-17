using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public GameObject Note_R;
    public GameObject Note_L;

    public float bpm = 240f;
    private Coroutine currentIsCoroutine;

    private Note scriptNoteR;
    private Note scriptNoteL;

    private AudioSource audioSource;
    private float time_Signatures = 0f;

    void Start()
    {
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
        time_Signatures = 60 / bpm * 8f;
        yield return new WaitForSeconds(time_Signatures*8-1f);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures/4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 4);
        yield return new WaitForSeconds(time_Signatures * 16);

        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteL.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
        scriptNoteR.Create_Note();
        yield return new WaitForSeconds(time_Signatures / 9);
    }
}
