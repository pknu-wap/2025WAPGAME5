using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void LateUpdate()
    {
        if (Interaction.gameStart)
        {

            gameObject.transform.position = new Vector3(0, 1, 0);


        }
    }
}
