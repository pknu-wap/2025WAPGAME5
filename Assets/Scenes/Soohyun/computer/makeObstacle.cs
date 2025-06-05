using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class makeObstacle : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject goal;
    public List<GameObject> spawnedobstacles = new List<GameObject>();
    public AudioSource codingbgmSource;
    bool noObstacle = true;
    public static bool clear = false;
    int level=1;
    // Start is called before the first frame update
    void Start()
    {
        codingbgmSource.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (clear)
        {
            Debug.Log("클리어함");
            foreach (GameObject obj in spawnedobstacles)
            {
                Destroy(obj);
            }
            spawnedobstacles.Clear();
            level++;
            clear = false;
            noObstacle=true;
        }
        if (noObstacle)
        {
            if (level==1)
            {
                StartCoroutine(WaitAndRun(new List<int>() { 1, 2, 3, 4, 12, 13, 14, 15, 17, 18, 19, 20, 22, 23, 24, 25 }));
            }
            else if (level==2)
            {
                StartCoroutine(WaitAndRun(new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 22, 23, 24, 25 }));
            }
            else if (level==3) // 게임 끝 
            {
                codingbgmSource.Stop();
                Debug.Log("코딩 점수 저장됨: " + collision.score);
                PlayerPrefs.SetInt("Score_Programming", collision.score);
                PlayerPrefs.SetInt("ReturnedFromProgramming", 1);
                SceneManager.LoadScene("ClassRoom");
            }
        }
    }
    IEnumerator WaitAndRun(List<int> pos)
    {
        //yield return new WaitForSeconds(0.5f);
        List<int> where = pos;//new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 22, 23, 24, 25 };
        foreach (int i in where)
            SpawnObstacle(i);
        SpawnGoal(5);
        noObstacle = false;
        yield return null;
    }
    void SpawnObstacle(int x)
    {
        GameObject obj = Instantiate(obstacle, new Vector3(10 * ((x - 1) % 5 - 2), -0.44f, 10 * (2 - Mathf.FloorToInt((x - 1) / 5))), Quaternion.identity);
        obj.name = obstacle.name+x;
        spawnedobstacles.Add(obj);
    }
    void SpawnGoal(int x)
    {
        GameObject obj = Instantiate(goal, new Vector3(10 * ((x - 1) % 5 - 2), -0.44f, 10 * (2 - Mathf.FloorToInt((x - 1) / 5))), Quaternion.identity);
        obj.name = goal.name+x;
        spawnedobstacles.Add(obj);
    }
}
