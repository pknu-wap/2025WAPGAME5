using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    [Header("속도 변화 설정")]
    public float speedBoostAmount = 5f;
    public float accelerationBoostAmount = 10f;
    public float duration = 3f;

    [Header("감정 변화 설정")]
    public EmotionType emotionToTrigger = EmotionType.normal;

    public enum EmotionType
    {
        normal = 0,
        Happy = 1,
        Angry = 2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            Debug.Log("속도 아이템 충돌");

            Car_to_school car = other.GetComponent<Car_to_school>();
            if (car != null)
            {
                car.ApplySpeedBoost(speedBoostAmount, accelerationBoostAmount, duration);
            }

            // 감정 변경 처리
            if (emotionToTrigger != EmotionType.normal)
            {
                FindObjectOfType<Emotion>()?.ChangeEmotion((int)emotionToTrigger);
            }

            Destroy(gameObject);
        }
    }
}
