using UnityEngine;
using UnityEngine.UI;

public class SitTrigger : MonoBehaviour
{
    public Transform seatPosition;
    public Player player;
    public Image actionImage;
    public GameObject point;

    private bool isPlayerNearby = false;
    private bool isSeated = false;

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
