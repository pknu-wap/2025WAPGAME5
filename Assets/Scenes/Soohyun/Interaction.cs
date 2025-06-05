using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public static bool gameStart = false;
    public static bool repeat = true;
    bool explain = false;

    public GameObject Player;
    public GameObject Table;
    public GameObject Button;
    public GameObject Slider1;
    public GameObject Slider2;
    public GameObject SliderDrink;
    public GameObject Text;
    public GameObject Point;

    float Dist;
    bool On = false;

    private Player playerScript;
    private Rigidbody rb;

    public static bool isGameEnded = false;

    void Start()
    {
        Slider1.SetActive(false);
        Slider2.SetActive(false);
        SliderDrink.SetActive(false);

        playerScript = Player.GetComponentInParent<Player>();
        rb = Player.GetComponent<Rigidbody>();
    }

    void Update()
    {



        if (explain)
        {
            Text.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                gameStart = true;
                Text.SetActive(false);
                explain = false;

                Slider1.SetActive(On);
                Slider2.SetActive(On);
                SliderDrink.SetActive(On);

            }
        }

        if (!gameStart && repeat)
        {
            Button_on();
            Dist = Vector3.Distance(Player.transform.position, Table.transform.position);

            if (Dist < 5)
            {
                On = true;

                if (Input.GetKeyDown("f"))
                {
                    Debug.Log("F pressed");

                    explain = true;
                    playerScript.SetDontMove(true);
                    rb.velocity = Vector3.zero;
                    Point.SetActive(false);

                    rb.MovePosition(new Vector3(-0.8f, 3.828f, -46.825f));

                    repeat = false;
                    Button.SetActive(!On);
                }
            }
            else
            {
                On = false;
            }
        }
    }

    void Button_on()
    {
        Button.SetActive(On);
    }

    public void GameEnd()
    {
        Debug.Log("¿òÁ÷¿©");
        gameStart = false;
        repeat = false;
        rb.velocity = Vector3.zero;

        rb.MovePosition(new Vector3(-2.47f, 2.71f, -51.82f));
        playerScript.SetDontMove(false);

        Slider1.SetActive(false);
        Slider2.SetActive(false);
        SliderDrink.SetActive(false);
        Button.SetActive(true);
        Button.SetActive(false);

        isGameEnded = true;
    }
}
