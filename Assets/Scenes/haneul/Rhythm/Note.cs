using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    public GameObject notePrefab;

    public void Create_Note()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }
}
