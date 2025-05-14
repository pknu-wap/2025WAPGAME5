using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject notePrefab;
    public bool isRight = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isRight)
        {
            Create_Note();
        }

        if (Input.GetKeyDown(KeyCode.J) && isRight)
        {
            Create_Note();
        }
    }

    public void Create_Note()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }
}