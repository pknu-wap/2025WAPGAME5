using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    [Header("속도 변화 설정")]
    public float speedBoostAmount = 5f;
    public float accelerationBoostAmount = 10f;
    public float duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            Debug.Log("충돌");
            Car_to_school car = other.GetComponent<Car_to_school>();

            car.ApplySpeedBoost(speedBoostAmount, accelerationBoostAmount, duration);

            Destroy(gameObject);
        }
    }
}
