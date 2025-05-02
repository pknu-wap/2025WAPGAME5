using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽� �߰�

public class NPCpunch : MonoBehaviour
{
    public float launchForceUp = 3f;              // ���� ƨ��� ��
    public float launchForceForward = 4f;         // ������ ƨ��� ��

    public float punchCooldown = 1.2f;              // ���� ��Ÿ�� (��)
    private float lastPunchTime = -Mathf.Infinity; // ������ ���� ����

    public TextMeshProUGUI cooldownText;          // TextMeshProUGUI ����

    void Update()
    {
        // ���� ��Ÿ�� ���
        float remainingCooldown = Mathf.Max(0f, (lastPunchTime + punchCooldown) - Time.time);

        // �ؽ�Ʈ ������Ʈ
        cooldownText.text = remainingCooldown > 0f
            ? $"{remainingCooldown:F1}"
            : "���� ����!";

        // ���콺 ��Ŭ�� & ��Ÿ�� Ȯ��
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

                    // ī�޶� �� ���� + �� �������� ���� ����
                    Vector3 launchDirection = Camera.main.transform.forward * launchForceForward + Vector3.up * launchForceUp;

                    rb.AddForce(launchDirection, ForceMode.Impulse);

                    // ������ ���� �ð� ������Ʈ
                    lastPunchTime = Time.time;
                }
            }
        }
    }
}
