using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public double loopStartTime = 2.22;  // 반복 시작시점
    public double loopEndTime = 5.0;    // 반복 끝 지점

    public Canvas canvas;

    public float breakfastTime;
    public float racingTime;
    public float alleyTime;

    public GameObject latePanel;
    public GameObject normalPanel;

    public GameObject pausePanel;
    private bool isPaused = false;
    private bool isSeeking = false;

    public ArrivalVideoController arrivalVideoController;
    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.isLooping = false;
            videoPlayer.loopPointReached += OnLoopPointReached;
            videoPlayer.Play();
        }
    }

    private void OnLoopPointReached(VideoPlayer vp)
    {
        StartCoroutine(SeekAndPlay(loopStartTime));
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
        if (currentScene != lastScene)
        {
            UpdateScene();
            lastScene = currentScene;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
    }
    private IEnumerator SeekAndPlay(double time)
    {
        isSeeking = true;
        videoPlayer.Pause();
        videoPlayer.time = time;

        yield return new WaitForSecondsRealtime(0.1f);

        videoPlayer.Play();
        isSeeking = false;
    }

    public void JudgeLateness()
    {
        float total = breakfastTime + racingTime + alleyTime;
        Debug.Log($"총 소요 시간: {total:F1}초, (120초 초과 시 지각!)");

        if (total > 120f)
        {
            Debug.Log("지각입니다!");
            PlayerPrefs.SetInt("IsLate", 1);
            arrivalVideoController.ShowArrivalVideo(true); // 지각 영상
        }
        else
        {
            Debug.Log("정상 등교!");
            PlayerPrefs.SetInt("IsLate", 0);
            arrivalVideoController.ShowArrivalVideo(false); // 정상 영상
        }
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