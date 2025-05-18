using UnityEngine;

public class EggFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform egg;
    private Vector3 offset;

    void Start()
    {
        if (egg != null)
            offset = transform.position - egg.position;
    }

    void LateUpdate()
    {
        if (egg != null)
        {
            Vector3 targetPos = egg.position + offset;
            targetPos.z = transform.position.z;
            transform.position = targetPos;
        }
    }
}
