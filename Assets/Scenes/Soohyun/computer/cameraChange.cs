using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera1;
    public Camera camera2;
    void Start()
    {
        camera2.rect = new Rect(0.75f, 0.75f, 0.25f, 0.25f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
