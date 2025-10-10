using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ImageInterface : MonoBehaviour
{
    public GameObject loader;
    public Image image;
    public VideoPlayer video;
    public RawImage rawImage;

    public GameObject videoObject;
    
    public RenderTexture rt;


    public void setImage(Sprite sprite)
    {
        image.gameObject.SetActive(true);
        image.sprite = sprite;
        loader.SetActive(false);
        if(videoObject != null)
        {
            videoObject.SetActive(false);
        }
        
    }

    public void setVideo(string link)
    {

        if (videoObject == null)
        {
            return;
        }
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();        
        videoObject.SetActive(true);
        video.url = link;
        video.targetTexture = rt;
        rawImage.texture = rt;
        video.Play();
        loader.SetActive(false);
        image.gameObject.SetActive(false);
        rt.Release();
    }


}
