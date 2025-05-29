using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicStarter : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] comicCuts;
    public float delay = 2f;
    public Button startButton;

    public void StartComic()
    {
        startButton.gameObject.SetActive(false);
        panel.SetActive(true);
        StartCoroutine(ShowComicsInPairs());
    }

    public IEnumerator ShowComicsInPairs()
    {
        for (int i = 0; i < comicCuts.Length; i += 2)
        {
            if (i < comicCuts.Length)
            {
                comicCuts[i].SetActive(true);
            }

            if (i + 1 < comicCuts.Length)
            {
                comicCuts[i + 1].SetActive(true);
            }

            yield return new WaitForSeconds(delay);
        }
        SceneManager.LoadScene("Juhyun");
    }
}
