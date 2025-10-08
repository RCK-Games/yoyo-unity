using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ProfileViewModel : ViewModel
{
    public User currentUser;
    public TextMeshProUGUI titleNameValueText, nameValueText, phoneValueText, emailValueText, idValueText, drinkValueText, foodValueText, musicValueText, pointsValueText;

    public Image avatarImage;
    void OnEnable()
    {
        SetInfo();
    }

    public void SetInfo()
    {
        currentUser = ApiManager.instance.GetUser();
        if (currentUser != null)
        {
            nameValueText.text = currentUser.name;
            phoneValueText.text = currentUser.related.phone;
            emailValueText.text = currentUser.email;
            idValueText.text = "ID: " + currentUser.id.ToString();
            drinkValueText.text = currentUser.related.taste_drink;
            foodValueText.text = currentUser.related.taste_food;
            musicValueText.text = currentUser.related.taste_music;
            pointsValueText.text = currentUser.related.points.ToString() + " points";
        }
    }

    public void ShowMediaPicker()
    {

        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                NewScreenManager.instance.ShowLoadingScreen(true);
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                byte[] imageBytes = texture.EncodeToJPG();
                string base64Image = System.Convert.ToBase64String(imageBytes);

                currentUser.avatar = base64Image;
                avatarImage.gameObject.SetActive(true);
                avatarImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                //ApiManager.instance.SetUser(currentUser);
                NewScreenManager.instance.ShowLoadingScreen(false);

             }
        }, "Select a picture", "image/*");


    }

    public void OnClickDeleteAccount()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.DeleteAccountViewModel, true);
    }

    public void OnClickOpenTastes()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.EditTastesViewModel, true);
        NewScreenManager.instance.GetCurrentView().GetComponent<EditTastesViewModel>().OnSetup(currentUser);
    }

    public void OnClickOpenProfile()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.EditProfileViewModel, true);
        NewScreenManager.instance.GetCurrentView().GetComponent<EditProfileViewModel>().OnSetup(currentUser);
    }

}
