using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClock : MonoBehaviour
{
    Rigidbody rigid;
    Player player;
    Bat bat;
    //Jump
    public int MaxJumpCnt;
    public float JumpForce;
    private int CurJumpCnt = 0;
    public float MaxJumpCoolTime;
    private float CurJumpCoolTime = 0f;
    private bool Jumping = false;

    //Vibration
    private float CurVibCoolTime = 0f;
    Vector3 InitPosition;
    public float VibrationSpeed;
    public float VibrationIntensity;

    //Hit
    bool IsHit = false;
    void Start()
    {
        rigid=GetComponent<Rigidbody>();
        InitPosition = transform.position;
        player = GetComponent<Player>();
    }
 
    void Update()
    {
        if(!IsHit)
            Jump();      
    }    

    void Jump()
    {
        if (rigid != null)
        {
            if (MaxJumpCnt < CurJumpCnt)
            {
                CurJumpCoolTime += Time.deltaTime;
                if (MaxJumpCoolTime <= CurJumpCoolTime)
                {
                    rigid.velocity = Vector3.zero;
                    CurJumpCnt = 0;
                    CurJumpCoolTime = 0;
                }
                else
                {
                    Vibration();
                }
            }
            
            //TODO : MaxJumpCnt���� ����ȵ�
            //���� : CurJumpCnt�� �ǵ���� ���� �ö��� �ʰ� �� �����Ӹ��� �����ϴ� ������ 
            //�� �� ������ ��������             
            if (IsGround() && CurJumpCnt <= MaxJumpCnt)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
                rigid.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                Jumping = true;                
            }            
            if(Jumping&&IsGround())
            {
                CurJumpCnt++;
                Jumping = false;
            }
        }
            
    }
    void Vibration()
    {
        float shakeX = Mathf.Sin(Time.time * VibrationSpeed) * VibrationIntensity;
        float shakeZ = Mathf.Cos(Time.time * VibrationSpeed) * VibrationIntensity;

        transform.position = InitPosition + new Vector3(shakeX, 0f, shakeZ);
    }
    bool IsGround()
    {        
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private void OnCollisionEnter(Collision b)
    {
        bat = b.gameObject.GetComponent<Bat>();
        //ChatGPT ��°�� ������
        //Ȥ�� ������� ������ �����....
        if (bat != null && bat.GetIsSwing())
        {
            float batForce = bat.GetSwingForce();

            // �浹 ������ ù ��° contact normal ���
            ContactPoint contact = b.contacts[0];
            Vector3 hitNormal = contact.normal;

            Vector3 incoming = b.transform.forward.normalized;
            Vector3 bounceDir = Vector3.Reflect(incoming, contact.normal).normalized;

            // ���� ���ϰ� Ƣ�� ���� �߰�
            Vector3 funnyBounce = (bounceDir + Vector3.up * 1.2f).normalized;

            // ��Ʈ �� ��Ƣ�� (�� ���ư���)
            float exaggerateForce = batForce * 7f; // �Ǵ� 10f �ص� ��

            // ���� �ӵ� �ʱ�ȭ (�� ���� �� ��)
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            // ƨ���!
            rigid.AddForce(funnyBounce * exaggerateForce, ForceMode.Impulse);

            IsHit = true;
        }
    }


}
