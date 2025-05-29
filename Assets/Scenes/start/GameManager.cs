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

    public float breakfastTime;
    public float racingTime;
    public float alleyTime;

    public GameObject pausePanel;
    private bool isPaused = false;

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

        // esc
        if (Input.GetKeyDown(KeyCode.Escape)&&!isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            AudioListener.pause = true;
            Time.timeScale = 0f; // 게임 일시정지

        }

    }

    public void JudgeLateness()
    {
        float total = breakfastTime + racingTime + alleyTime;
        Debug.Log($"총 소요 시간: {total:F1}초, (120초 초과 시 지각입니다)");

        if (total > 120f)
            Debug.Log("지각입니다!");
        else
            Debug.Log("정상 등교!");
    }


    public void Continue()
    {
        if (isPaused)
        {
            Debug.Log("이어하기");
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    public void Exit()
    {

        Application.Quit();
        #if UNITY_EDITOR
    // 에디터에서는 플레이 모드를 종료함
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

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