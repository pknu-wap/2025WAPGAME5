using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    public GameObject notePrefab;

    public void Create_Note()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);

        if (Input.GetKeyDown(KeyCode.BackQuote)) //숫자 1 왼쪽의 ~키
        {
            PlayerPrefs.SetInt("ReturnedFromMusic", 1);
            SceneManager.LoadScene("ClassRoom");
        }
    }
}
