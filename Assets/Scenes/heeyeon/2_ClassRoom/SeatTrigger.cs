using UnityEditor.Experimental.GraphView;
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

    private bool isPlayerNearby = false;
    private bool isSeated = false;
    private bool readyToStartKorean = false;


    void Start()
    {
        actionImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && !isSeated)
        {
            actionImage.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                point.gameObject.SetActive(false);
                SitDown();
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

            if (readyToStartKorean && Input.GetKeyDown(KeyCode.Space))
            {
                //종소리 이때 나게
                SceneManager.LoadScene("korean");
            }
        }
    }

    private void SitDown()
    {
        isSeated = true;
        player.SetDontMove(true);

        player.transform.position = seatPosition.position;
        player.transform.rotation = seatPosition.rotation;
        player.FixCameraRotation(seatPosition.rotation);

        actionImage.gameObject.SetActive(false);

        Invoke(nameof(Changeboard), 1f);
    }

    private void Changeboard()
    {
        Renderer rend = board.GetComponent<Renderer>();
        if (rend != null && koreanClassTexture != null)
        {
            rend.material.mainTexture = koreanClassTexture;
        }

        readyToStartKorean = true;
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
}
