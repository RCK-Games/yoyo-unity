using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class VideoInterface : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public List<VideoClip> clips;

    public GameObject loader;

    void Start()
    {
        if (clips.Count > 0)
        {
            videoPlayer.clip = clips[0];
            StartCoroutine(waitForVideoToPrepare());

        }
    }

    IEnumerator waitForVideoToPrepare()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoPlayer.Play();
        loader.SetActive(false);

    }

    public void SetClip(int index)
    {
        if (index >= 0 && index < clips.Count)
        {
            videoPlayer.clip = clips[index];
            StartCoroutine(waitForVideoToPrepare());
        }
    }
}
