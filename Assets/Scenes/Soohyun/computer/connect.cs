using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class connect : MonoBehaviour
{
    Coroutine myCoroutine;
    public GameObject robot;
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject button11;
    public GameObject button2;
    public GameObject restart;
    public float moveSpeed = 20f;
    public static int limit=10;
    public RectTransform start;
    public static Vector2 startpos;
    public static Vector3 speed;
    public static bool canStart =true ;
    public static bool running = true;
    public static bool stop = false;
    public static bool moving = false;
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
            running = true;

            children.Clear();
            stop = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canStart /*|| makeObstacle.clear*/)
        {
            canvas.SetActive(false);
            canStart = false; 
            camera1.gameObject.SetActive(false);
            restart.SetActive(false);
            camera2.gameObject.SetActive(true);
            camera2.targetTexture = null;
            rawImageUI.enabled = false;
            button2.SetActive(false);


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
                //canStart=true;
                restart.SetActive(true);
                button2.SetActive(false);


            }

        }
        //실패했을때 
        if (result)
        {
            result = false;
            restart.SetActive(true);
            button11.SetActive(true);
            if (!makeObstacle.clear)
            {
                collision.score -= 10;
                Debug.Log(collision.score);

            }
            Debug.Log(collision.score);
        }

    }
    IEnumerator ActionCoroutine()
    {
        List<Transform> childrenCopy = new List<Transform>(children);

        foreach (Transform child in childrenCopy)
        {
            Debug.Log(child.name);
            Vector3 startpos = robot.transform.position;
            float movedistance;
            float totalangle = 0f;
            float moveangle;
            moving = true;
            Vector3 rotate = robot.transform.eulerAngles;
            while (running)
            {
                //앞으로 이동
                if (child.tag == "forward")
                {
                    speed= robot.transform.forward * moveSpeed * Time.deltaTime;
                    robot.transform.position += robot.transform.forward * moveSpeed * Time.deltaTime;
                    movedistance = Vector3.Distance(robot.transform.position, startpos);
                    if (movedistance > 10)
                    {
                        running = false;
                        Vector3 direction = (robot.transform.position - startpos).normalized;
                        robot.transform.position = startpos + direction * 10;
                        moving = false;
                    }
                }
                //왼쪽 90도
                else if (child.tag == "left")
                {
                    Debug.Log("왼쪽 90도");
                    moveangle = 90 * Time.deltaTime;
                    transform.Rotate(0, -moveangle, 0);
                    totalangle -= moveangle;
                    if (rotate.y > 180)
                    {
                        rotate.y -= 360;
                    }
                    float robotAngle = robot.transform.eulerAngles.y;
                    if (robotAngle >= 180)
                    {
                        robotAngle -= 360;
                    }
                    float angle = rotate.y - robotAngle;
                    if (angle < 0)
                        angle += 360; 
                    Debug.Log(angle);
                    if (angle > 90)
                    {
                        Debug.Log("끝");
                        running = false;
                        robot.transform.eulerAngles =rotate+ new Vector3(0, -90, 0);

                        moving = false;
                    }
                }
                //오른쪽 90도
                else if (child.tag == "right")
                {
                    moveangle = 90 * Time.deltaTime;
                    transform.Rotate(0, moveangle, 0);
                    totalangle += moveangle;
                    if (rotate.y > 180)
                    {
                        rotate.y -= 360;
                    }
                    float robotAngle = robot.transform.eulerAngles.y;
                    if (robotAngle > 180)
                    {
                        robotAngle -= 360;
                    }
                    float angle = robotAngle - rotate.y;
                    if (angle < 0)
                        angle += 360;
                    if (angle > 90)
                    {
                        running = false;
                        robot.transform.eulerAngles = rotate + new Vector3(0, 90, 0);

                        moving = false;
                    }
                }
                else
                {
                    Debug.Log($"알 수 없는 태그: {child.tag}");
                    running = false;
                }
                yield return null;
            }

            Debug.Log(child + "끝");
            yield return new WaitForSeconds(0.5f);
            running = true;
        }
        if(!collision.next)
            result = true;
        //canStart = true;
        canvas2.SetActive(true);
        restart.SetActive(true);
        children.Clear();
        connect.moving = false;
    }

}
