using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Emotion;
    public GameObject Mission;
    public GameObject Crosshair;

    public static int currentEmotion = 0;
    public static int currentMission = 0;
    public static int lastScene = 0;
    public static int currentScene= 0;

    public VideoPlayer videoPlayer;
    public double loopStartTime = 2.22;  // 반복 시작 지점 (초)
    public double loopEndTime = 5.0;    // 반복 끝 지점 (초)
    private bool hasLooped = false;

    public Canvas canvas;

    public float breakfastTime;
    public float racingTime;
    public float alleyTime;

    public GameObject latePanel;
    public GameObject normalPanel;

    public GameObject pausePanel;
    private bool isPaused = false;
    void Start()
    {
        videoPlayer.isLooping = false; // 기본 루프 끄기
        videoPlayer.Play();
    }

    private void Awake()
    {// 씬 넘어가도 유지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (videoPlayer != null)
        {
            if (videoPlayer.isPrepared && videoPlayer.time >= loopEndTime)
            {
                hasLooped = true;
                videoPlayer.Pause(); // 먼저 멈추고
                videoPlayer.time = loopStartTime;
                videoPlayer.Play();
            }

            if (!videoPlayer.isPlaying && hasLooped)
            {
                videoPlayer.time = loopStartTime;
                videoPlayer.Play();
            }
        }

        /*if (Input.GetKeyDown(KeyCode.F))
        {
            //currentEmotion++;
            
            if (currentEmotion >= 3) // 감정 개수
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
        }*/

        //SceneManager
        if (currentScene != lastScene)
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
        Debug.Log($"총 소요 시간: {total:F1}초, (120초 초과 시 지각!)");

        if (total > 120f)
        {
            Debug.Log("지각입니다!");
            latePanel.SetActive(true);
            PlayerPrefs.SetInt("IsLate", 1);
            Invoke("HideLatePanel", 2f); 
        }
        else
        {
            Debug.Log("정상 등교!");
            normalPanel.SetActive(true);
            PlayerPrefs.SetInt("IsLate", 0);
            Invoke("HideNormalPanel", 2f); 
        }
    }

    void HideLatePanel()
    {
        latePanel.SetActive(false);
    }

    void HideNormalPanel()
    {
        normalPanel.SetActive(false);
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