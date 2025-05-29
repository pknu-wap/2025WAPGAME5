using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class connect : MonoBehaviour
{
    Coroutine myCoroutine;
    public GameObject robot;
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject button11;
    public GameObject button2;
    public GameObject restart;
    public float moveSpeed = 10f;
    public static int limit=10;
    public RectTransform start;
    public static Vector2 startpos;
    public static bool canStart =true ;
    public static bool running = true;
    public static bool stop = false;
    public bool result = false;
    public List<Transform> children = new List<Transform>();
    public Camera camera1;
    public Camera camera2;
    public RawImage rawImageUI;
    public RenderTexture renderTex;

    void Update()
    {
        if (stop)
        {
            transform.position = new Vector3(-20, 1.1f, -20);
            transform.rotation = Quaternion.identity;

            if (myCoroutine != null)
                StopCoroutine(myCoroutine);

            Debug.Log("정지");
            //restart.SetActive(false);
            button11.SetActive(true);

            children.Clear();
            stop = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canStart || makeObstacle.clear)
        {
            canvas.SetActive(false);
            canStart = false; 
            camera1.gameObject.SetActive(false);
            restart.SetActive(false);
            camera2.gameObject.SetActive(true);
            camera2.targetTexture = null;
            rawImageUI.enabled = false;
            button2.SetActive(false);

            //transform.position = new Vector3(-20, 1.1f, -20);
            //transform.rotation = Quaternion.identity;

            children.Clear();
            foreach (Transform child in start.transform.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("forward")|| child.CompareTag("left")|| child.CompareTag("right"))
                {
                    children.Add(child);
                }
            }
            Debug.Log("움직일 횟수:" + children.Count);
            Debug.Log("움직이는중");
            if (Input.GetKeyDown(KeyCode.Space) &&children.Count <= limit && children.Count > 0)
            {
                myCoroutine = StartCoroutine(ActionCoroutine());

            }
            else
            {
                Debug.Log("못움직임");
                canStart=true;
                restart.SetActive(true);
                button2.SetActive(false);


            }

        }
        //실패했을때 
        if (Input.GetKeyDown(KeyCode.Space) && result )
        {
            result = false;
            restart.SetActive(false);
            button11.SetActive(true);
            //restart.SetActive(false);
            //button11.SetActive(true);
        }

    }
    IEnumerator ActionCoroutine()
    {

        foreach (Transform child in children)
        {
            Debug.Log(child.name);
            Vector3 startpos = robot.transform.position;
            float movedistance ;
            float totalangle = 0f;
            float moveangle ;
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
                        float yangle = (float)Math.Truncate(robot.transform.eulerAngles.y);
                        if ((yangle%90)!=0)
                            {
                            yangle = Mathf.FloorToInt(yangle / 90) * 90 + 90;
                            }

                        robot.transform.eulerAngles = new Vector3(0, (int)yangle, 0);
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
                    running = false;  
                    yield return null;
                }
            }
            Debug.Log(child+"끝");
            yield return new WaitForSeconds(0.5f);
            running = true ;
        }
        result = true;
        canStart = true;
        canvas2.SetActive(true);
        restart.SetActive(true);
        //button2.SetActive(false);
        children.Clear(); 
    }
}
