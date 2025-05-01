using System.Drawing;
using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool isDrinking = false;
    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = LayerMask.GetMask("Water");
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            Debug.Log(hit.transform.name);
            if (!isDrinking && Interaction.gameStart)
            {
                Debug.Log("²Ü²© ²Ü²©");
                isDrinking = true;
            }
        }
    }

}