using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInteractionTriggerBox : MonoBehaviour
{
    Player player;
    MyClock clock;
    Bat bat;
    void Start()
    {
        clock=FindObjectOfType<MyClock>();
        bat = FindObjectOfType<Bat>();
    }

    bool isPlayerInside = false;

    void Update()
    {
        //if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        //{
        //    clock.SetClockOff(true);   
            
        //}
    }

    private void OnTriggerEnter(Collider p)
    {
        if (p.GetComponent<Player>() != null)
            isPlayerInside = true;
    }

    private void OnTriggerExit(Collider p)
    {
        if (p.GetComponent<Player>() != null)
            isPlayerInside = false;
    }


    private void OnTriggerStay(Collider p)
    {
        if (clock.GetClockOff())
            return;
        player = p.GetComponent<Player>();        
        if (player != null)
        {
            bool IsMoving = false;            
            player.SetDontMove(true);
            if (Input.GetKeyDown(KeyCode.E))
            {                                            
                clock.SetClockOff(true);
                IsMoving = true;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (bat.GetIsBatEquipped())
                {
                    bat.DoSwing();
                }
                IsMoving = true;
            }
            if(IsMoving)
            {
                player.SetDontMove(false);
            }            
        }
    }

}