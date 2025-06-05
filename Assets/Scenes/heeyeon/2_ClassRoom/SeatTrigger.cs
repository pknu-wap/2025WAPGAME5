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
    public Texture defaultBoardTexture;
    public Texture koreanClassTexture;
    public Texture scienceClassTexture;
    public Texture programmingClassTexture;
    public Texture lunchTexture;
    public Texture historyClassTexture;
    public Texture mathClassTexture;
    public Texture musicClassTexture;

    public AudioClip bellSound;
    public AudioSource bellSource;

    private bool isPlayerNearby = false;
    private bool isSeated = false;
    private bool readyToStartClass = false;
    private string nextScene = "";

    void Start()
    {
        FindObjectOfType<GameManager>().Emotion.SetActive(false);
        FindObjectOfType<GameManager>().Mission.SetActive(false);
        FindObjectOfType<GameManager>().Crosshair.SetActive(false);

        actionImage.gameObject.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;

        SitDownIfReturned();
    }

    void SitDownIfReturned()
    {
        if (PlayerPrefs.GetInt("ReturnedFromKorean", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromKorean", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareScience), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromScience", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromScience", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareProgramming), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromProgramming", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromProgramming", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareLunch), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromLunch", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromLunch", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareHistory), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromHistory", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromHistory", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareMath), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromMath", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromMath", 0);
            SitDown(true);
            ResetToDefaultBoard();
            Playbell();
            Invoke(nameof(PrepareMusic), 3f);
        }
        else if (PlayerPrefs.GetInt("ReturnedFromMusic", 0) == 1)
        {
            PlayerPrefs.SetInt("ReturnedFromMusic", 0);
            SceneManager.LoadScene("EndingScene");
            return;
        }
    }
    void Playbell()
    {
        if (bellSound != null && bellSource != null)
            bellSource.PlayOneShot(bellSound);
    }
    void Update()
    {
        if (isPlayerNearby && !isSeated)
        {
            actionImage.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                point.SetActive(false);
                SitDown(false);
                Playbell();
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
                string key = "ReturnedFrom" + char.ToUpper(nextScene[0]) + nextScene.Substring(1);
                PlayerPrefs.SetInt(key, 1);
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
        player.SetLockCamera(true);

        actionImage.gameObject.SetActive(false);

        if (!fromReturn)
        {
            Invoke(nameof(PrepareKorean), 1f);
        }
    }

    private void PrepareKorean()
    {
        SetBoardTexture(koreanClassTexture);
        readyToStartClass = true;
        nextScene = "korean";
    }

    private void PrepareScience()
    {
        SetBoardTexture(scienceClassTexture);
        readyToStartClass = true;
        nextScene = "Science";
    }

    private void PrepareProgramming()
    {
        SetBoardTexture(programmingClassTexture);
        readyToStartClass = true;
        nextScene = "school";
    }

    private void PrepareLunch()
    {
        SetBoardTexture(lunchTexture);
        readyToStartClass = true;
        nextScene = "Lunch";
    }

    private void PrepareHistory()
    {
        SetBoardTexture(historyClassTexture);
        readyToStartClass = true;
        nextScene = "History";
    }

    private void PrepareMath()
    {
        SetBoardTexture(mathClassTexture);
        readyToStartClass = true;
        nextScene = "Math";
    }

    private void PrepareMusic()
    {
        SetBoardTexture(musicClassTexture);
        readyToStartClass = true;
        nextScene = "Rhythm_Game";
    }


    private void ResetToDefaultBoard()
    {
        SetBoardTexture(defaultBoardTexture);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
