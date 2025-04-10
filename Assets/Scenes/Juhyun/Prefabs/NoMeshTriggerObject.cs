using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMeshTriggerObject : MonoBehaviour
{
    private bool IsPlayerInsideTriggerBox = false;
    Player player;    

    private void OnTriggerEnter(Collider p)
    {
        player = p.GetComponent<Player>();
        if (player != null)
        {
            IsPlayerInsideTriggerBox = true;
        }
    }
    public bool GetIsPlayerInsideTriggerBox()
    {
        return IsPlayerInsideTriggerBox;
    }
}
