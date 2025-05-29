using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SitTrigger : MonoBehaviour
{
    public Transform seatPosition;
    public Player player;
    public Image actionImage;
    public GameObject point;

    public GameObject board;
    public Texture koreanClassTexture;
    public Texture defaultBoardTexture;
    public Texture scienceClassTexture;

    private bool isPlayerNearby = false;
    private bool isSeated = false;
    private bool readyToStartClass = false;
    private string nextScene = "";

    void Start()
    {
        actionImage.gameObject.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // 국어 씬에서 돌아온 경우 처리
        if (PlayerPrefs.GetInt("ReturnedFromKorean", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromKorean", 0);
            SitDown(true);  // true면 첫 앉기 아님, 곧바로 앉고 진행
            ResetToDefaultBoard();
            Invoke(nameof(PrepareNextClass), 3f); // 3초 뒤 과학 인트로로 변경
        }
    }

    void Update()
    {
        if (isPlayerNearby && !isSeated)
        {
            actionImage.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                point.gameObject.SetActive(false);
                SitDown(false); // false는 첫 앉기
            }
        }
        else
        {
            actionImage.gameObject.SetActive(false);
        }

        if (isSeated)
        {
            player.transform.position = seatPosition.position;
            player.transform.rotation = seatPosition.rotation;
            player.FixCameraRotation(seatPosition.rotation);

            if (readyToStartClass && Input.GetKeyDown(KeyCode.Space))
            {
                if (nextScene == "korean")
                {
                    PlayerPrefs.SetInt("ReturnedFromKorean", 1); // 국어 끝났다는 표시
                }
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void SitDown(bool fromReturn = false)
    {
        point.SetActive(false);
        isSeated = true;
        player.SetDontMove(true);

        player.transform.position = seatPosition.position;
        player.transform.rotation = seatPosition.rotation;
        player.FixCameraRotation(seatPosition.rotation);

        actionImage.gameObject.SetActive(false);

        if (!fromReturn)
        {
            // 첫 앉기면 1초 뒤 국어 인트로
            Invoke(nameof(ChangeboardToKorean), 1f);
        }
    }

    private void ChangeboardToKorean()
    {
        SetBoardTexture(koreanClassTexture);
        readyToStartClass = true;
        nextScene = "korean";
    }

    private void ResetToDefaultBoard()
    {
        SetBoardTexture(defaultBoardTexture);
    }

    private void PrepareNextClass()
    {
        SetBoardTexture(scienceClassTexture);
        readyToStartClass = true;
        nextScene = "science";
    }

    private void SetBoardTexture(Texture texture)
    {
        Renderer rend = board.GetComponent<Renderer>();
        if (rend != null && texture != null)
        {
            rend.material.mainTexture = texture;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == player)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() == player)
        {
            isPlayerNearby = false;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 로드 시 필요한 추가 작업 있으면 여기에
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
