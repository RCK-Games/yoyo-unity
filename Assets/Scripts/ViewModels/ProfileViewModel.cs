using UnityEngine;
using TMPro;
public class ProfileViewModel : ViewModel
{
    public User currentUser;
    public TextMeshProUGUI titleNameValueText, nameValueText, phoneValueText, emailValueText, idValueText, drinkValueText, foodValueText, musicValueText, pointsValueText;
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
