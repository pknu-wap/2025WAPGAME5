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
        ReEat();
        if (!canEat && curGauge <= 95)
            canEat = true;
        curGauge -= 2 * Time.deltaTime;
        if (curGauge < 0)
            curGauge = 0;
        CheckUp();
        if (curGauge == 100) //* 이미 체력 0이하면 패스
            canEat = false;
        SliderEat.value = curGauge / maxGauge;
        SliderDrink.value = DrinkGauge / maxGauge;
    }
    public void CheckUp() //* 데미지 받는 함수
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
}
