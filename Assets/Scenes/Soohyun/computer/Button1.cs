using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class button1 : MonoBehaviour
{
    public Button button11;
    public Button button22;
    public Button button33;
    public Camera camera1;
    public Camera camera2;
    public GameObject canvas;
    public RawImage rawImageUI;
    public RenderTexture renderTex;
    public TextMeshProUGUI nextLevel;
    private void Awake()
    {
        Canvas parentCanvas = GetComponentInParent<Canvas>();
        Vector3 canvaspos = parentCanvas.transform.position;
        camera1.transform.position = new Vector3(canvaspos.x, canvaspos.y, -canvaspos.x);
    }
    void Start()
    {
        button11.onClick.AddListener(MyFunction1);
        button22.onClick.AddListener(MyFunction2);
        button33.onClick.AddListener(MyFunction3);
    }

    void MyFunction1()
    {
        canvas.SetActive(false);
        button11.gameObject.SetActive(false);
        button22.gameObject.SetActive(true);
        Debug.Log("화면2");
        camera2.rect = new Rect(0f, 0f, 1f, 1f);
        camera2.targetTexture = null; 
        rawImageUI.enabled = false;
    }
    void MyFunction2()
    {
        canvas.SetActive(true);
        button22.gameObject.SetActive(false);
        button11.gameObject.SetActive(true);
        Debug.Log("화면1");
        camera1.gameObject.SetActive(true);
        camera2.targetTexture = renderTex;
        rawImageUI.enabled = true;
    }
    void MyFunction3()
    {
        connect.stop= true;
        connect.canStart = true;
        canvas.SetActive(true);
        camera1.gameObject.SetActive(true);
        button33.gameObject.SetActive(false);
        camera2.targetTexture = renderTex;
        rawImageUI.enabled = true;
        nextLevel.text = "다시하기";
    }
}
