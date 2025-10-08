using UnityEngine;
using UnityEngine.UI;
public class ImageInterface : MonoBehaviour
{
    public GameObject loader;
    public Image image;



    public void setImage(Sprite sprite)
    {
        image.sprite = sprite;
        loader.SetActive(false);
    }


}
