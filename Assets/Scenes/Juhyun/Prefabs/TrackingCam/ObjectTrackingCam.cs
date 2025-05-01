using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrackCam : MonoBehaviour
{
    public Camera TrackingCam;
    private bool IsMoving = false;
    Player player;
    private Camera PlayerCamera; //main camera
    private Vector3 InitPlayerCamPos;
    private Quaternion InitPlayerCamRot;
    private bool CamBack = false;
    public float TrackingSpeed=1f;
    private bool IsFirstTrigger = false;
    private bool IsMoreThanOnceTrigger = false;
    void Start()
    {
        if(TrackingCam!=null)
        {
            TrackingCam.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (TrackingCam != null&& PlayerCamera != null)
        {
            if(IsFirstTrigger&&!IsMoreThanOnceTrigger)
            {
                float step = TrackingSpeed * Time.deltaTime;
             
                if (!CamBack)
                {
                    PlayerCamera.transform.position = Vector3.Lerp(PlayerCamera.transform.position, TrackingCam.transform.position, step);
                    PlayerCamera.transform.rotation = Quaternion.Slerp(PlayerCamera.transform.rotation, TrackingCam.transform.rotation, step);

                    if (Vector3.Distance(PlayerCamera.transform.position, TrackingCam.transform.position) < 0.1f)
                    {
                        CamBack = true;
                    }
                }
                else
                {
                    PlayerCamera.transform.position = Vector3.Lerp(PlayerCamera.transform.position, InitPlayerCamPos, step * 2f);
                    PlayerCamera.transform.rotation = Quaternion.Slerp(PlayerCamera.transform.rotation, InitPlayerCamRot, step * 2f);

                    if (Vector3.Distance(PlayerCamera.transform.position, InitPlayerCamPos) < 0.001f)
                    {
                        CamBack = false;
                        IsMoreThanOnceTrigger = true;
                        player.SetDontMove(false);
                    }
                }
            }            
        }
    }
    private void OnTriggerEnter(Collider p)
    {
        player = p.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("충돌한 오브젝트에 Player 컴포넌트가 없습니다.");
            return; // player가 없으면 아무것도 하지 않음
        }

        if (IsFirstTrigger)
        {
            IsMoreThanOnceTrigger = true;
            return;
        }

        if (!IsFirstTrigger)
        {
            PlayerCamera = player.GetComponentInChildren<Camera>();
            IsFirstTrigger = true;

            if (PlayerCamera != null)
            {
                player.SetDontMove(true);
                InitPlayerCamPos = PlayerCamera.transform.position;
                InitPlayerCamRot = PlayerCamera.transform.rotation;
            }
            else
            {
                Debug.LogWarning("Player 하위에 Camera가 없습니다.");
            }
        }
    }

}
