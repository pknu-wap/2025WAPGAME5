using UnityEngine;
using UnityEngine.Video;

public class ArrivalVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoUI;
    public VideoClip normalClip;
    public VideoClip lateClip;

    public void ShowArrivalVideo(bool isLate)
    {
        videoUI.SetActive(true);
        videoPlayer.clip = isLate ? lateClip : normalClip;
        videoPlayer.time = 0;
        videoPlayer.Play();

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoUI.SetActive(false);
        videoPlayer.loopPointReached -= OnVideoEnd;
        //]]]]]]]]
    }
}
