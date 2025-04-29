using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour
{
    public GameObject startUI, endUI;
    private bool isPlaying = false;
    private float startTime;
    private GameObject player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 0;
        startUI.SetActive(true);
        endUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        startUI.SetActive(false);
        Time.timeScale = 1;
        isPlaying = true;
        startTime = Time.time;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (player != null)
        {
            player.GetComponent<Player22>().SetPlaying(true);
        }
    }

    public void EndGame()
    {
        if (isPlaying)
        {
            isPlaying = false;
            float playTime = Time.time - startTime;
            Debug.Log("플레이 시간: " + playTime + "초");

            endUI.SetActive(true);
            if (player != null)
            {
                player.GetComponent<Player22>().SetPlaying(false);
            }

            StartCoroutine(HideEndUI());
        }
    }

    IEnumerator HideEndUI()
    {
        yield return new WaitForSecondsRealtime(2f);
        endUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
        {
            player.GetComponent<Player22>().SetPlaying(true);
        }
    }
}
