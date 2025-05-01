using UnityEngine;
public class Interaction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool gameStart = false;
    public static bool repeat = true;
    public GameObject Player;
    public GameObject Table;
    public GameObject Button;
    public GameObject Slider1;
    public GameObject Slider2;
    public GameObject SliderDrink;
    float Dist;
    bool On = false;
    // Update is called once per frame
    void Start()
    {
        Slider1.SetActive(false);
        Slider2.SetActive(false);
        SliderDrink.SetActive(false);
    }
    void Update()
    {
        if (!gameStart && repeat)
        {
            Button_on();
            Dist = Vector3.Distance(Player.transform.position, Table.transform.position);
            if (Dist < 5)
            {
                On = true;
                if (Input.GetKey("f"))
                {
                    gameStart = true;
                    Player.transform.position = new Vector3(-1, 5, -48);
                    repeat = false;
                    Cursor.visible = true;
                    Slider1.SetActive(On);
                    Slider2.SetActive(On);
                    SliderDrink.SetActive(On); 
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

}