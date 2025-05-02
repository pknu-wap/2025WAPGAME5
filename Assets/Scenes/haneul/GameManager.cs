using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    public GameObject Emotion;
    public static int currentEmotion = 0;
    public GameObject Mission;
    public static int currentMission = 0;
    public static int lastScene = 0;
    public static int currentScene= 0;
    public static int scoreGTS = 0;//학교도착까지의 점수
    public Canvas canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (currentScene == 0)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //currentEmotion++;

            
            if (currentEmotion >= 7) // 감정 개수
            {
                currentEmotion = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //currentMission++;


            if (currentMission >= 4) // 미션 수 넣기
            {
                currentMission = 0;
            }
        }

        //SceneManager
        if(currentScene != lastScene)
            {
                UpdateScene();
                lastScene = currentScene;
            }

    }

    public void UpdateScene()
    {
        switch (currentScene)
        {
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
            case 3:
                SceneManager.LoadScene(3);
                break;
            case 4:
                SceneManager.LoadScene("Scene4");
                break;
            default:
                break;
        }
    }


}