using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    public GameObject restart;
    public TextMeshProUGUI nextLevel;
    public static int score =100;
    public static bool next =false;
    // Start is called before the first frame update
    void Update()
    {
        if (transform.position.y < -4)
        {
            //PlayerReset();
            Debug.Log("실패"); 
            transform.position = new Vector3(-20, 1.1f, -20);
            transform.rotation = Quaternion.identity;
            connect.stop = true;
            restart.SetActive(true);
            //score -= 10;
            connect.moving = false;
        }
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
           // PlayerReset();
            Debug.Log("실패");
            //connect.stop = true;
            restart.SetActive(true);
            score -= 10;
            Debug.Log(score);
        }
        else if (collision.collider.CompareTag("goal"))
        {
            Debug.Log("레벨 클리어!");
            //PlayerReset();
            //transform.position = new Vector3(-20, 1.1f, -20);
            //transform.rotation = Quaternion.identity;
            //connect.stop = true;
            nextLevel.gameObject.SetActive(true);
            next = true;
            nextLevel.text = "다음 레벨";
        }
    }
    private void LateUpdate()
    {
        if (score <= 0)
        {
            score = 0;
        }

        //Debug.Log(score);
    }
}

