using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ImageInterface : MonoBehaviour
{
    public GameObject loader;
    public Image image;
    public VideoPlayer video;
    public RawImage rawImage;

    public Button button;

    public GameObject videoObject;
    
    public RenderTexture rt;


    public void setImage(Sprite sprite)
    {
        image.gameObject.SetActive(true);
        image.sprite = sprite;
        loader.SetActive(false);
        if (videoObject != null)
        {
            videoObject.SetActive(false);
        }
        image.preserveAspect = true;

    }

    public void setAdLink(string link)
    {
        button.enabled = true;
        button.onClick.AddListener(() => Application.OpenURL(link));
    }
    
    IEnumerator waitForVideoToPrepare()
    {
        video.Prepare();
        while (!video.isPrepared)
        {
            yield return null;
        }
        video.Play();
        loader.SetActive(false);
        
    }

    public void setVideo(string link)
    {
        if (videoObject == null)
        {
            return;
        }
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        rt.Create();
        videoObject.SetActive(true);
        video.url = link;
        video.targetTexture = rt;
        rawImage.texture = rt;
        image.gameObject.SetActive(false);
        rt.Release(); 
        StartCoroutine(waitForVideoToPrepare());
    }


}
