using System;
using UnityEngine;
using UnityEngine.UI;

public class EatFood : MonoBehaviour
{

    float eatStart=0;
    float eatFinish;
    float totalEatTime;
    int a = 1;
    public int eat;
    public GameObject Food1;
    public GameObject Food2;
    public GameObject Food3;
    public GameObject Food4;
    public GameObject Food5;
    public GameObject particle;
    public GameObject Space;
    public Slider FoodGauge;
    float b;
    void Awake()
    {
        eatStart = Time.time;
        /*Time.deltaTime*/
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void FixedUpdate()
    {

        /*Time.deltaTime*/
    }
    void Update()
    {
        FoodGauge.value = (float)eat / 100;
        OnMouseDown();
        if (eatStart == 0 )
        {
            eatStart = Time.time;
        }
        if (eat == 20)
        {
            
            Food1.SetActive(false);
            particle.transform.position = Food2.transform.position;

        }
        else if (eat == 40)
        {
            Food2.SetActive(false);
            particle.transform.position = Food3.transform.position;

        }
        else if (eat == 60)
        {
            Food3.SetActive(false);
            particle.transform.position = Food4.transform.position;

        }
        else if (eat == 80)
        {
            Food4.SetActive(false);
            particle.transform.position = Food5.transform.position;

        }
        else if (eat == 100)
        {
            FoodGauge.value = 1f;
            eatFinish = Time.time;
            totalEatTime = eatFinish - eatStart;
            Food5.SetActive(false);
            particle.SetActive(false);
            FoodGauge.gameObject.SetActive(false);

        }
    }
    void OnDisable()
    {
        while (true)
        {
            if (totalEatTime < a * 10)
                break;
            a++;
        }
        Debug.Log(totalEatTime);
        //Debug.Log("����, �� �Ծ���. " + a * 5 + "�� �ɷȳ�");
        GameManager.currentMission += 1;
        Interaction.gameStart = false;
    }
    void OnMouseDown()
    {
        if (Interaction.gameStart)
        {
            if (Input.GetKeyDown("space") && Gauge.canEat && !DrinkWater.isDrinking)
            {
                Debug.Log(++eat);
                Space.SetActive(false);
                b = 0;

            }
            else
            {
                b+=Time.deltaTime;
                if (b > 0.1)
                {
                    b = 0;
                    Space.SetActive(true);
                }
            }
        }
    }

}


