using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class NPCpunch : MonoBehaviour
{
    public float launchForceUp = 3f;              
    public float launchForceForward = 4f;        

    public float punchCooldown = 1.2f;    
    private float lastPunchTime = -Mathf.Infinity; 
    public TextMeshProUGUI cooldownText;   

    void Update()
    {
        float remainingCooldown = Mathf.Max(0f, (lastPunchTime + punchCooldown) - Time.time);

        cooldownText.text = remainingCooldown > 0f
            ? $"{remainingCooldown:F1}"
            : "공격 가능!";

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

                    Vector3 launchDirection = Camera.main.transform.forward * launchForceForward + Vector3.up * launchForceUp;

                    rb.AddForce(launchDirection, ForceMode.Impulse);

                    lastPunchTime = Time.time;
                }
            }
        }
    }
}
