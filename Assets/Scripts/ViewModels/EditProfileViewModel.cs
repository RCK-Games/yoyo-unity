using UnityEngine;
using TMPro;
public class EditProfileViewModel : ViewModel
{
    public TMP_InputField nameInputText, phoneInputText;
    private User currentUser;
    public TextMeshProUGUI countryValueText, namePlaceHolder, phonePlaceHolder;
    public PhoneSelectorHandler phoneSelectorHandler;
    public void OnSetup(User _currentUser)
    {
        currentUser = _currentUser;
        namePlaceHolder.text = currentUser.name;
        
        if (currentUser.related.phone.Length > 0)
        {
            Debug.Log(currentUser.related.phone.Split(' '));
            phonePlaceHolder.text = currentUser.related.phone.Split(' ')[1];
            phoneSelectorHandler.SearchSpecificCountry(currentUser.related.phone.Split(' ')[0].Replace("(", "").Replace(")", "").Replace("+", ""));
        }
    }

    public void SaveNewProfileInfo()
    {
        
        if (nameInputText.text != "")
        {
            currentUser.name = nameInputText.text;
        }else{
            currentUser.name = namePlaceHolder.text;
        }
        if (phoneInputText.text != "")
        {
            currentUser.related.phone = $"{countryValueText.text} {phoneInputText.text}";
        }else{
            currentUser.related.phone = $"{countryValueText.text} {phonePlaceHolder.text}";
        }
        Debug.Log(phoneInputText.text);
        Debug.Log(nameInputText.text);
        Debug.Log(phonePlaceHolder.text);
        Debug.Log(namePlaceHolder.text);
        Debug.Log(currentUser.related.phone);
        Debug.Log(currentUser.name);
        ApiManager.instance.SetUser(currentUser);
        NewScreenManager.instance.BackToPreviousView();
        NewScreenManager.instance.GetCurrentView().GetComponent<ProfileViewModel>().SetInfo();
    }
}
