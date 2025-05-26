using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class connect : MonoBehaviour
{
    public GameObject robot;
    public GameObject canvas;
    public float moveSpeed = 10f;
    public RectTransform start;
    public static Vector2 startpos;
    public bool canStart =true ;
    public bool running = true;
    public List<Transform> children = new List<Transform>();

    //bool isMoving = false;
    //bool isRotating = false;
    //List<GameObject> children =new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canStart)
        {
            
            canStart = false;
            foreach (Transform child in start.transform.GetComponentsInChildren<Transform>())
            {
                if (child != transform)  // 자기 자신은 제외
                {
                    children.Add(child);
                }
            }
            StartCoroutine(ActionCoroutine());
            Debug.Log("끝");
        }
    }
    IEnumerator ActionCoroutine()
    {
        Debug.Log("시작");

        
        Debug.Log(children);
        foreach (Transform child in children)
        {
            Debug.Log(start.transform.childCount);
            if (child == start.transform)
            {
                continue;
            }
            canvas.SetActive(false);
            Debug.Log(child.name);
            Vector3 startpos = robot.transform.position;
            float movedistance = 0f;
            float totalangle = 0f;
            float moveangle = 0f;
            while (running)
            {

                if (child.tag == "forward")
                {
                    robot.transform.position += robot.transform.forward * moveSpeed * Time.deltaTime;
                    //10만큼 이동하면 중지
                    movedistance = Vector3.Distance(robot.transform.position, startpos);
                    if (movedistance > 10)
                    {
                        running = false;
                        Vector3 direction = (robot.transform.position - startpos).normalized;
                        Debug.Log(direction);
                        robot.transform.position = startpos + direction * 10;
                    }
                    yield return null;

                }
                else if (child.tag == "left")
                {
                    moveangle = 90 * Time.deltaTime;
                    transform.Rotate(0, -moveangle, 0);
                    totalangle -= moveangle;
                    if (totalangle < -90)
                    {
                        running = false;
                        float yangle = Mathf.FloorToInt(Mathf.Abs(robot.transform.eulerAngles.y));
                        if ((yangle%90)!=0)
                            {
                            yangle = Mathf.FloorToInt(yangle / 90) * 90 + 90;
                            }

                        robot.transform.eulerAngles = new Vector3(0, yangle, 0);
                    }
                    yield return null;

                }
                else if (child.tag == "right")
                {
                    moveangle = 90 * Time.deltaTime;
                    transform.Rotate(0, moveangle, 0);
                    totalangle += moveangle;
                    if (totalangle > 90)
                    {
                        running = false;
                        robot.transform.eulerAngles = new Vector3(0, Mathf.FloorToInt(robot.transform.eulerAngles.y), 0);
                    }
                    yield return null;

                }
                else
                {
                    // 예외 처리 또는 대기
                    Debug.LogWarning($"알 수 없는 태그: {child.tag}");
                    running = false;  // 잘못된 태그일 경우 루프 종료 (또는 yield return null;)
                    yield return null;
                }
            }
            Debug.Log(child+"끝");
            yield return new WaitForSeconds(0.5f);
            running = true ;
        }
        canStart = true;
        canvas.SetActive(true);
        children.Clear();
    }
}
