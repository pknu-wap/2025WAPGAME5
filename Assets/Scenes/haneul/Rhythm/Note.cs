using System.Collections;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject notePrefab;

    public void Create_Note()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }
}
