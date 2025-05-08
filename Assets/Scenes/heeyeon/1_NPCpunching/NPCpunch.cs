using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class NPCpunch : MonoBehaviour
{
    public float launchForceUp = 3f;              // 위로 튕기는 힘
    public float launchForceForward = 4f;         // 앞으로 튕기는 힘

    public float punchCooldown = 1.2f;              // 공격 쿨타임 (초)
    private float lastPunchTime = -Mathf.Infinity; // 마지막 공격 시점

    public TextMeshProUGUI cooldownText;          // TextMeshProUGUI 참조

    void Update()
    {
        // 남은 쿨타임 계산
        float remainingCooldown = Mathf.Max(0f, (lastPunchTime + punchCooldown) - Time.time);

        // 텍스트 업데이트
        cooldownText.text = remainingCooldown > 0f
            ? $"{remainingCooldown:F1}"
            : "공격 가능!";

        // 마우스 좌클릭 & 쿨타임 확인
        if (Input.GetMouseButtonDown(0) && remainingCooldown <= 0f)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = false;

                    // 카메라 앞 방향 + 위 방향으로 힘을 가함
                    Vector3 launchDirection = Camera.main.transform.forward * launchForceForward + Vector3.up * launchForceUp;

                    rb.AddForce(launchDirection, ForceMode.Impulse);

                    // 마지막 공격 시간 업데이트
                    lastPunchTime = Time.time;
                }
            }
        }
    }
}
