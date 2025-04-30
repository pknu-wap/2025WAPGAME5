using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// UI�� ���� �ð� ����, ���콺 Ŀ�� ����ֽ��ϴ�. 
public class Manager : MonoBehaviour
{
    public GameObject startUI, endUI;
    public Texture2D fistCursor;
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

        Cursor.SetCursor(fistCursor, Vector2.zero, CursorMode.Auto);

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
            Debug.Log("�÷��� �ð�: " + playTime + "��");

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
