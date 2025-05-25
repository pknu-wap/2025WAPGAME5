using UnityEngine;

public class LateManager : MonoBehaviour
{
    public static LateManager Instance;

    public float breakfastTime;
    public float racingTime;
    public float alleyTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void JudgeLateness()
    {
        float total = breakfastTime + racingTime + alleyTime;
        Debug.Log($"총 소요 시간: {total:F1}초, (120초 초과 시 지각입니다)");

        if (total > 120f)
            Debug.Log("지각입니다!");
        else
            Debug.Log("정상 등교!");
    }
}
