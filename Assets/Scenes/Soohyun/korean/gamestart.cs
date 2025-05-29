using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamestart : MonoBehaviour
{
    public GameObject input;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        input.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Ω√¿€");
            sentence script = canvas.GetComponent<sentence>();
            script.enabled = true;
            input.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
