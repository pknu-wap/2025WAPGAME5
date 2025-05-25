using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class connect : MonoBehaviour
{
    public GameObject robot;
    public GameObject canvas;
    public float moveSpeed = 10f;
    public RectTransform start;
    public static Vector2 startpos;
    public bool gameStart   =false ;

    //bool isMoving = false;
    //bool isRotating = false;
    //List<GameObject> children =new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || gameStart)
        {
            //if (!isMoving && !isRotating) {
            foreach (Transform child in start.transform.GetComponentsInChildren<Transform>())
            {
                Debug.Log(child.name);
                Vector3 childPos = child.transform.position;
                canvas.SetActive(false);
                StartCoroutine(ActionCoroutine(child));
                gameStart = false;
            }
            canvas.SetActive(true);
        }
    }
    IEnumerator ActionCoroutine(Transform child)
    {
        yield return new WaitForSeconds(1f);

        if (child.tag == "forward")
        {
            transform.position += transform.forward;
            Debug.Log("앞으로");
        }
        else if (child.tag == "left")
        {
            Debug.Log("왼쪽으로");
            transform.eulerAngles += new Vector3(0f, -90f, 0f);

        }
        else if (child.tag == "right")
        {
            Debug.Log("오른쪽으로");
            transform.eulerAngles += new Vector3(0f, 90f, 0f);

        }
    }
}
