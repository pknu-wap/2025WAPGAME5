using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Movement : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(0f, -6f, -2f);
    private float duration = 2f;



    void Start()
    {
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



        Destroy(gameObject,0.2f);
    }
}
