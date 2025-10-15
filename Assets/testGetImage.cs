using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class testGetImage : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI bytes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public void OnClick()
    {

                string base64Image = System.Convert.ToBase64String(SpriteToByteArray(avatarImage.sprite));
                UploadImageRequest uploadData = new UploadImageRequest();
        uploadData.image = "data:image/png;base64," + base64Image;
                Debug.Log(base64Image);
                ApiManager.instance.UpdateUsersImage(uploadData, (object[] response) =>
                    {
                        long responseCode = (long)response[0];
                        string responseText = response[1].ToString();
                        Debug.Log(responseText);
                        if (responseCode == 200)
                        {
                            Medium imageResponse = JsonUtility.FromJson<Medium>(responseText);
                            avatarImage.gameObject.SetActive(true);
                            ApiManager.instance.SetImageFromUrl(imageResponse.absolute_url, (Sprite response) =>
                            {
                                avatarImage.sprite = response;
                            });
                        }
                        else
                        {
                            avatarImage.gameObject.SetActive(false);
                        }
                        NewScreenManager.instance.ShowLoadingScreen(false);
                    });

                
            

    }



    public static Texture2D MakeTextureReadable(Texture2D texture)
    {
        RenderTexture tmp = RenderTexture.GetTemporary(
            texture.width,
            texture.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
        );

        Graphics.Blit(texture, tmp);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmp;

        Texture2D readableTex = new Texture2D(texture.width, texture.height);
        readableTex.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        readableTex.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmp);

        return readableTex;
    }

    public static byte[] SpriteToByteArray(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogError("Sprite nulo");
            return null;
        }
        Texture2D texture = sprite.texture;

        Texture2D croppedTexture = new Texture2D(
            (int)sprite.rect.width,
            (int)sprite.rect.height,
            TextureFormat.RGBA32,
            false
        );

        Color[] pixels = texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        byte[] bytes = croppedTexture.EncodeToPNG();

        Object.Destroy(croppedTexture);

        return bytes;
    }
}
