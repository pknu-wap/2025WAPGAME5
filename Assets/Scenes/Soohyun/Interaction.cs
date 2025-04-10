using UnityEngine;
public class Interaction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool gameStart = false;
    public GameObject Player;
    public GameObject Table;
    float Dist;
    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            Dist = Vector3.Distance(Player.transform.position, Table.transform.position);
            if (Input.GetKey("f") && Dist < 3)
            {
                Debug.Log("클릭! 거리:" + Dist);
                gameStart = true;
                Cursor.visible = true;
            }
        }
    }

}