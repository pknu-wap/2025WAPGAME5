using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject endUI;
    private bool isPlaying = false;
    private float startTime;
    private GameObject player;

    void Start()
    {
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
        startTime = Time.time; // 시간 측정 시작 일단 넣긴 넣..

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

        if (player != null)
        {
            player.GetComponent<Player22>().SetPlaying(true);
        }
    }
}
