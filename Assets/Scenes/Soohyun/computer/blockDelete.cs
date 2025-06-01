using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class blockDelete : MonoBehaviour
{
    public List<Transform> delete = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void Update()
    {
        //foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        //{
        //    if (!child.gameObject.activeSelf)
        //    {
        //        Debug.Log(child.name);
        //        if (child.CompareTag("forward") || child.CompareTag("left") || child.CompareTag("right"))
        //        {

        //            Debug.Log("ªË¡¶" + child);
        //            child.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}
