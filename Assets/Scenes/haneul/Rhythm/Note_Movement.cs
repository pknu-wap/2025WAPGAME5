using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Movement : MonoBehaviour
{
    public float x_Pos = 0f;
    private Vector3 targetPosition;
    private float duration = 1.5f;



    void Start()
    {
        targetPosition = new Vector3(x_Pos, -6f, -2f);
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;



        Destroy(gameObject,0.1f);
    }
}
