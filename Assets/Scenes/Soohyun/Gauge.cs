using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public float curGauge;
    public int maxGauge;
    public float DrinkGauge;
    public static bool canEat = true;
    public Slider SliderEat;
    public Slider SliderDrink;

    public Player player; 

     

    void Start()
    {
        curGauge = 0;
        DrinkGauge = 0;
        maxGauge = 100;

        
        
    }

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

                if (player != null)
                {
                    player.SetDontMove(false);
                }

                
                
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
