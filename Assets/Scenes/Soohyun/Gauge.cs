using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public float curGauge; //* 현재 게이지
    public int maxGauge; //* 최대 게이지
    public float DrinkGauge;
    public static bool canEat = true;
    public Slider SliderEat;
    public Slider SliderDrink;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curGauge = 0;
        DrinkGauge = 0;
        maxGauge = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Interaction.gameStart)
        {
            ReEat();
            if (!canEat && curGauge <= 95)
                canEat = true;
            curGauge -= 2 * Time.deltaTime;
            if (curGauge < 0)
                curGauge = 0;
            CheckUp();
            if (curGauge == 100)
            {
                GameManager.currentEmotion = 6;
                canEat = false;
            }
            SliderEat.value = curGauge / maxGauge;
            SliderDrink.value = DrinkGauge / maxGauge;
        }
    }
    public void CheckUp()
    {
        if (Input.GetKeyDown("space") && canEat && !DrinkWater.isDrinking)
        {

            curGauge += 2;
            if (curGauge >= 100)
                curGauge = 100;
        }
    }
    void ReEat()
    {
        if (DrinkWater.isDrinking)
        {
            //curGauge -= 5*Time.deltaTime ;
            DrinkGauge += 50 * Time.deltaTime;
            if (DrinkGauge > 100)
            {
                DrinkGauge = 0;
                DrinkWater.isDrinking = false;
                curGauge -= 20;
            }


        }
    }
    private void OnDisable()
    {
        SliderEat.gameObject.SetActive(false);
        SliderDrink.gameObject.SetActive(false);
    }
}
