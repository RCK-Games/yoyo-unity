using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class testVideo : MonoBehaviour
{
    

    public VideoPlayer video;
    public RawImage rawImage;


    private RenderTexture rt;
    
    void Start() {
        setVideo("https://admin.yoyotheclub.com/storage/advertisements/imGIvizO7r7k1dVrFef4WeQhTzWyXS6w2EjH45Tv.mp4");
    }


    public void setVideo(string link)
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
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
