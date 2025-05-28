using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunchTouch : MonoBehaviour
{
    public CatMove catMove;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            catMove.Move();
        }
    }
}
