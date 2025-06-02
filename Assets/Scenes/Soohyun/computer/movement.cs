using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody m_rigid;
    Animator m_anim;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationParameters();
    }
    public void UpdateAnimationParameters()
    {

        m_anim.SetBool("isMoving", connect.moving);
    }

}
