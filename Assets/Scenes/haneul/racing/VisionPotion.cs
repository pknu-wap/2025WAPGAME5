using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionPotion : MonoBehaviour
{
    public GameObject blindImage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            StartCoroutine(ActivateBlindness());
        }
    }

    private IEnumerator ActivateBlindness()
    {
        FindObjectOfType<Emotion>()?.ChangeEmotion(2);
        if (blindImage != null)
            blindImage.SetActive(true);

        yield return new WaitForSeconds(3f);


        if (blindImage != null)
            blindImage.SetActive(false);


        Destroy(gameObject);
    }
}
