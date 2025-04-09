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

    void Start()
    {
        if(TrackingCam!=null)
        {
            TrackingCam.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (TrackingCam != null&&IsMoving)
        {

        }
    }
    private void OnTriggerEnter(Collider p)
    {        
        if (p.GetComponent<Player>().tag == "Player")
        {
            player = GetComponent<Player>();
            PlayerCamera=player.GetComponentInChildren<Camera>();
            IsMoving = true;
            InitPlayerCamPos = PlayerCamera.transform.position;
            InitPlayerCamRot = PlayerCamera.transform.rotation;
        }
    }
}
