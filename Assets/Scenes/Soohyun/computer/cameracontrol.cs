using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameracontrol : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    // Start is called before the first frame update
    private void Awake()
    {
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        Vector3 canvaspos = parentCanvas.transform.position;
        camera1.transform.position = new Vector3(canvaspos.x, canvaspos.y, -canvaspos.x);
    }
    private void OnEnable()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }
}
