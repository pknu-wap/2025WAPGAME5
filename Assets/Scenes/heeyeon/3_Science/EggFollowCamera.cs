using UnityEngine;

public class EggFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform egg;
    [SerializeField] private Transform targetLine; 

    private Vector3 offset;
    private float minY; 

    void Start()
    {
        if (egg != null)
            offset = transform.position - egg.position;

        float cameraHeight = Camera.main.orthographicSize * 2f;
        minY = targetLine.position.y - (cameraHeight / 2f);
    }

    void LateUpdate()
    {
        if (egg != null)
        {
            Vector3 targetPos = egg.position + offset;
            targetPos.z = transform.position.z;

            targetPos.y = Mathf.Max(targetPos.y, minY);

            transform.position = targetPos;
        }
    }
}
