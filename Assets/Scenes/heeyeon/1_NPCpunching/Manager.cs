using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// UI랑 게임 시간 측정, 마우스 커서 기능있습니다. 
public class Manager : MonoBehaviour
{
    public GameObject startUI, endUI;
    public Texture2D fistCursor;
    public bool isPlaying = false;
    private float startTime;
    private GameObject player;
    public GameObject PunchCoolDown;

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

        Cursor.SetCursor(fistCursor, Vector2.zero, CursorMode.Auto);

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