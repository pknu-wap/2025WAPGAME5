using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject W;
    public GameObject A;
    public GameObject S;
    public GameObject D;
    public GameObject Shift;
    public GameObject Key;
    public GameObject door;

    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;
    private bool shiftPressed = false;

    private bool isTutorialActive = false;

    public void Tuto()
    {
        isTutorialActive = true;
        W.SetActive(true);
        A.SetActive(true);
        S.SetActive(true);
        D.SetActive(true);
        Shift.SetActive(true);
        Key.SetActive(true);

        wPressed = false;
        aPressed = false;
        sPressed = false;
        dPressed = false;
        shiftPressed = false;
    }

    void Update()
    {
        if (!isTutorialActive) return;

        if (!wPressed && Input.GetKeyDown(KeyCode.W))
        {
            wPressed = true;
            W.SetActive(false);
        }
        if (!aPressed && Input.GetKeyDown(KeyCode.A))
        {
            aPressed = true;
            A.SetActive(false);
        }
        if (!sPressed && Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
            S.SetActive(false);
        }
        if (!dPressed && Input.GetKeyDown(KeyCode.D))
        {
            dPressed = true;
            D.SetActive(false);
        }
        if (!shiftPressed && Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftPressed = true;
            Shift.SetActive(false);
        }

        if (wPressed && aPressed && sPressed && dPressed && shiftPressed)
        {
            isTutorialActive = false;
            Key.SetActive(false);
            door.SetActive(false);
            Debug.Log("튜토리얼 완료");
        }
    }
}
