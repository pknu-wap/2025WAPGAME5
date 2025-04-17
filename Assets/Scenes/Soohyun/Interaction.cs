using UnityEngine;
public class Interaction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool gameStart = false;
    public GameObject Player;
    public GameObject Table;
    public GameObject Button;
    public GameObject Slider1;
    public GameObject Slider2;
    float Dist;
    bool On = false;
    // Update is called once per frame
    void Start()
    {
        Slider1.SetActive(false);
        Slider2.SetActive(false);
    }
    void Update()
    {
        if (!gameStart)
        {
            Button_on();
            Dist = Vector3.Distance(Player.transform.position, Table.transform.position);
            if (Dist < 3)
            {
                On = true;
                if (Input.GetKey("f"))
                {
                    gameStart = true;
                    Cursor.visible = true;
                    On = false;
                    Slider1.SetActive(!On);
                    Slider2.SetActive(!On);
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

}