using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cameracontrol : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public RawImage rawImageUI;
    public RenderTexture renderTex;
    // Start is called before the first frame update
    private void Awake()
    {
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        Vector3 canvaspos = parentCanvas.transform.position;
        camera1.transform.position = new Vector3(canvaspos.x, canvaspos.y, -canvaspos.x);
    }
    //private void OnEnable()
    //{
    //    camera1.gameObject.SetActive(true);
    //    //camera2.gameObject.SetActive(false);
    //    camera2.targetTexture = renderTex;
    //    rawImageUI.enabled = true;

    //}

    //// Update is called once per frame
    //private void OnDisable()
    //{
    //    camera1.gameObject.SetActive(false);
    //    camera2.gameObject.SetActive(true);
    //    camera2.targetTexture = null;
    //    rawImageUI.enabled = false;
    //}
}
