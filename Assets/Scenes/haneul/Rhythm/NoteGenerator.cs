using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteGenerator : MonoBehaviour
{
    public GameObject Note_R;
    public GameObject Note_L;

    public float bpm = 240f;
    private Coroutine currentPatternCoroutine;

    private Note scriptNoteR; // 전역 멤버 변수로 선언
    private Note scriptNoteL;

    void Start()
    {
        scriptNoteR = Note_R.GetComponent<Note>();
        scriptNoteL = Note_L.GetComponent<Note>();
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
        if (currentPatternCoroutine != null)
        {
            StopCoroutine(currentPatternCoroutine);
        }
        currentPatternCoroutine = StartCoroutine(PlayPattern(pattern));
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

        currentPatternCoroutine = null;
    }
}
