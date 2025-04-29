using UnityEngine;

public class NPCpunch : MonoBehaviour
{
    public float launchForceUp = 6f;
    public float launchForceForward = 9f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = false;
                    Vector3 launchDirection = Camera.main.transform.forward * launchForceForward + Vector3.up * launchForceUp;
                    rb.AddForce(launchDirection);
                }
            }
        }
    }
}