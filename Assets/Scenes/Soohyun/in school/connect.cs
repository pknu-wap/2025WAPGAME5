using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class connect : MonoBehaviour
{
    public GameObject robot;
    public float moveSpeed = 10f;
    public RectTransform start;
    public static Vector2 startpos;
    //bool isMoving = false;
    //bool isRotating = false;
    //List<GameObject> children =new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = start.anchoredPosition;

        Debug.Log("start 좌표" +  start.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (!isMoving && !isRotating) {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
                if (child != transform)  // 자기 자신은 제외
                {
                    //children.Add(child.gameObject);
                    if (child.tag == "forward")
                    {
                        Vector3 childPos = child.transform.position;
                        Debug.Log("앞으로");
                        StartCoroutine(ForwardCoroutine(child.gameObject, 10f));
                    }
                    else if (child.tag == "left")
                    {
                        Debug.Log("왼쪽으로");

                    }
                    else if (child.tag == "right")
                    {
                        Debug.Log("오른쪽으로");

                    }
                }
        }
    }
    IEnumerator ForwardCoroutine(GameObject obj, float distance)
    {
        Vector3 startPos = obj.transform.position;
        Vector3 direction = obj.transform.forward; // 현재 앞 방향
        Vector3 targetPos = startPos + direction * distance;

        while (Vector3.Distance(obj.transform.position, targetPos) > 0.01f)
        {
            Debug.Log(Vector3.Distance(obj.transform.position, targetPos));
            obj.transform.position = Vector3.MoveTowards(
                obj.transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            yield return null; // 다음 프레임까지 기다림
        }

        // 최종 위치 보정
        obj.transform.position = targetPos;
        Debug.Log("이동 완료");
    }
}
