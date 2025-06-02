using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EatFood : MonoBehaviour
{
    float eatStart = 0;
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

    private Interaction interaction;

    void Awake()
    {
        eatStart = Time.time;
        interaction = FindObjectOfType<Interaction>();
    }

    void Update()
    {
        if (FoodGauge != null)
        {
            FoodGauge.value = (float)eat / 100;
        }

        OnMouseDown();

        if (eatStart == 0)
        {
            eatStart = Time.time;
        }

        if (eat == 20)
        {
            if (Food1 != null)
                Food1.SetActive(false);

            if (particle != null && Food2 != null)
                particle.transform.position = Food2.transform.position;
        }
        else if (eat == 40)
        {
            if (Food2 != null)
                Food2.SetActive(false);

            if (particle != null && Food3 != null)
                particle.transform.position = Food3.transform.position;
        }
        else if (eat == 60)
        {
            if (Food3 != null)
                Food3.SetActive(false);

            if (particle != null && Food4 != null)
                particle.transform.position = Food4.transform.position;
        }
        else if (eat == 80)
        {
            if (Food4 != null)
                Food4.SetActive(false);

            if (particle != null && Food5 != null)
                particle.transform.position = Food5.transform.position;
        }
        else if (eat >= 99)
        {
            if (FoodGauge != null)
            {
                FoodGauge.value = 1f;
                FoodGauge.gameObject.SetActive(false);
            }

            eatFinish = Time.time;
            totalEatTime = eatFinish - eatStart;

            if (Food5 != null)
                Food5.SetActive(false);

            if (particle != null)
                particle.SetActive(false);
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

        Debug.Log($"1단계 아침밥:{totalEatTime:F2}초");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.breakfastTime = totalEatTime;
        }

        GameManager.currentMission += 1;
        Interaction.gameStart = false;

        if (interaction != null)
        {
            interaction.GameEnd();
        }
        else
        {
            Debug.LogWarning("Interaction script not found!");
        }

        Debug.Log("끝");
    }

    void OnMouseDown()
    {
        if (Interaction.gameStart)
        {
            if (Input.GetKeyDown("space") && Gauge.canEat && !DrinkWater.isDrinking)
            {
                eat += 1;
                Debug.Log(eat);
                if (Space != null)
                    Space.SetActive(false);

                b = 0;
            }
            else
            {
                b += Time.deltaTime;
                if (b > 0.1f)
                {
                    b = 0;
                    if (Space != null)
                        Space.SetActive(true);
                }
            }
        }
    }
}
