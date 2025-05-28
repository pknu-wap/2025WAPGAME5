using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class collision : MonoBehaviour
{
    public static bool moving = false;
    // Start is called before the first frame update
    void Update()
    {

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(-20, 1.5f, -20);
            transform.rotation = Quaternion.identity;
            connect.stop=true;
            Debug.Log("실패");

        }
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
            StopAllCoroutines();
            transform.position=new Vector3(-20,1.5f,-20);
            transform.rotation = Quaternion.identity;
            connect.stop = true;
            Debug.Log("실패");
        }
        else if (collision.collider.CompareTag("goal"))
        {
            Debug.Log("레벨 클리어!");
            StopAllCoroutines();
            transform.position=new Vector3(-20,1.5f,-20);
            transform.rotation = Quaternion.identity;
            connect.stop = true;
            makeObstacle.clear = true;
        }
    }
}

