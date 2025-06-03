using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note_Movement : MonoBehaviour
{
    public float x_Pos = 0f;
    private Vector3 targetPosition;
    private float duration = 1.5f;
    public GameObject missObjectPrefab;

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
        Vector3 spawnPosition = transform.position + new Vector3(0f, 1.95f, 1f);
        
        yield return new WaitForSeconds(0.1f);
        Instantiate(missObjectPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("miss");
        Rhythm_Judgement.Rhythm_Score -= 1;
        Destroy(gameObject);
    }
}
