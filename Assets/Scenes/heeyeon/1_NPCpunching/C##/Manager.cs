using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// UI랑 게임 시간 측정, 마우스 커서 기능있습니다. 
public class Manager : MonoBehaviour
{
    public GameObject startUI, endUI;
    public bool isPlaying = false;
    private float startTime;
    private GameObject player;
    public GameObject PunchCoolDown;

    public AudioSource bgmSource;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 0f;
        GameManager.isPausingInGame = true;
        startUI.SetActive(true);
        endUI.SetActive(false);
    }

    void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.isPausingInGame = false;
            StartGame();
            bgmSource.Play();
            PunchCoolDown.SetActive(true);
        }
    }

    void StartGame()
    {
        PunchCoolDown.SetActive(true);
        startUI.SetActive(false);
        Time.timeScale = 1;
        isPlaying = true;
        startTime = Time.time;

        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
        Cursor.visible = false;                   // 커서 숨김

        if (player != null)
        {
            player.GetComponent<Player22>().SetPlaying(true);
        }
    }

    public void EndGame()
    {
        PunchCoolDown.SetActive(false);
        if (isPlaying)
        {
            isPlaying = false;
            float playTime = Time.time - startTime;

            Debug.Log($"3단계 골목길:{playTime:F2}초");

            bgmSource.Stop();

            endUI.SetActive(true);
            if (player != null)
            {
                player.GetComponent<Player22>().SetPlaying(false);
            }

            StartCoroutine(HideEndUI());
            GameManager.Instance.alleyTime = playTime;
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