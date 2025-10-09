using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections.Generic;

public class VideoInterface : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public List<VideoClip> clips;

    void Start()
    {
        if (clips.Count > 0)
        {
            videoPlayer.clip = clips[0];
            videoPlayer.Play();
        }
    }

    public void SetClip(int index)
    {
        if (index >= 0 && index < clips.Count)
        {
            videoPlayer.clip = clips[index];
            videoPlayer.Play();
        }
    }
}
