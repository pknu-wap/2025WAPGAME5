using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    Player player;
    Camera MainCam;
    private bool IsBatEquipped = false;
    private bool SwingForward = true;
    private float SwingProgress = 0f;
    public float SwingSpeed = 1f;
    bool IsSwinging = false;
    public float SwingForce;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsBatEquipped)
        {
            IsSwinging = true;
        }
        if (IsSwinging)
        {
            DoSwing();
        }
    }

    public void DoSwing()
    {
        if(IsBatEquipped)
        {
            SwingProgress += 0.005f * SwingSpeed;
            if (SwingForward)
            {
                if (SwingProgress >= 1f)
                {
                    SwingProgress = 0f;
                    SwingForward = false;
                    return;
                }
                transform.localRotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(90, 0, 0), SwingProgress);
            }            
            else
            {
                if (SwingProgress >= 1f)
                {
                    SwingProgress = 0f;
                    SwingForward = true;
                    IsSwinging = false;
                    return;
                }
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(90, 0, 0),Quaternion.identity, SwingProgress);
            }            
        }
    }

    private void OnCollisionEnter(Collision p)
    {
        player=p.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            MainCam = player.GetComponentInChildren<Camera>();
            transform.SetParent(MainCam.transform);            
            transform.localPosition = new Vector3(0.3f, -0.3f, 0.5f);
            transform.localRotation = Quaternion.identity;            

            IsBatEquipped = true;
        }
    }

    public bool GetIsSwing()
    {
        return IsSwinging;
    }

    public float GetSwingForce()
    {
        return SwingForce;
    }

    public bool GetIsBatEquipped()
    {
        return IsBatEquipped;
    }
}
