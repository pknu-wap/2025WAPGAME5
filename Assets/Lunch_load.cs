using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lunch_load : MonoBehaviour
{
    public Image loadingImage;
    public float fillSpeed = 0.5f; 

    private void Update()
    {
        if (loadingImage == null) return;

        loadingImage.fillAmount += fillSpeed * Time.deltaTime;

        if (loadingImage.fillAmount >= 1f)
        {
            loadingImage.fillAmount = 0f;
        }
    }
}
