using UnityEngine;

public class Player22 : MonoBehaviour
{
    private bool isPlaying = false;

    public void SetPlaying(bool playing)
    {
        isPlaying = playing;
        Debug.Log("Player22 isPlaying set to: " + isPlaying);
    }

    /*public bool IsPlaying()
    {
        return isPlaying;
    } //���� Ȯ�ο�*/
}
