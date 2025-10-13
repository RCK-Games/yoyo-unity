using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class testVideo : MonoBehaviour
{
    

    public VideoPlayer video;
    public RawImage rawImage;


    public RenderTexture rt;
    
    void Start() {
        setVideo("http://64.227.105.243/storage/events/HP9FY9Unjzvf4Yqb3yXZK6K0jPrurxF3QQYSGjXi.mp4");
    }


    public void setVideo(string link)
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();
        video.url = link;
        video.targetTexture = rt;
        rawImage.texture = rt;
        video.Play();
        rt.Release();
    }
    
    public void OnClick()
    {
        rawImage.gameObject.SetActive(!rawImage.gameObject.activeSelf);
    }
}
