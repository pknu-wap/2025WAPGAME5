using System;
using UnityEngine;
using UnityEngine.UI;

public class EatFood : MonoBehaviour
{

    float eatStart;
    float eatFinish;
    float totalEatTime;
    int a = 1;
    public int eat;
    public GameObject Food1;
    public GameObject Food2;
    public GameObject Food3;
    public GameObject Food4;
    public GameObject Food5;
    public Slider FoodGauge;
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
        if (eat == 20)
        {
            
            Food1.SetActive(false);

        }
        else if (eat == 40)
        {
            Food2.SetActive(false);

        }
        else if (eat == 60)
        {
            Food3.SetActive(false);

        }
        else if (eat == 80)
        {
            Food4.SetActive(false);

        }
        else if (eat == 100)
        {
            FoodGauge.value = 1f;
            eatFinish = Time.time;
            totalEatTime = eatFinish - eatStart;
            Food5.SetActive(false);

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
        Debug.Log("²¨¾ï, Àß ¸Ô¾ú´Ù. " + a * 5 + "ºÐ °É·È³×");
    }
    void OnMouseDown()
    {

        if (Input.GetKeyDown("space") && Gauge.canEat && !DrinkWater.isDrinking)
        {
            Debug.Log(++eat);

        }
    }

}


