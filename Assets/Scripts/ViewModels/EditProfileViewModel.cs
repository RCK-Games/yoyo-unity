using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EditProfileViewModel : ViewModel
{
    public TMP_InputField nameInputText, phoneInputText;
    private User currentUser;
    public TextMeshProUGUI countryValueText, namePlaceHolder, phonePlaceHolder;
    public PhoneSelectorHandler phoneSelectorHandler;
    public GameObject clearNameButton, clearPhoneButton;

    public Button SaveButton;
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

    public void ClearNameInput()
    {
        nameInputText.text = "";
        clearNameButton.SetActive(false);
        SaveButton.interactable = false;
    }

    public void ClearPhoneInput()
    {
        phoneInputText.text = "";
        clearPhoneButton.SetActive(false);
        SaveButton.interactable = false;
    }

    public void OnChangeNameInput()
    {
        if (nameInputText.text.Length > 0)
        {
            clearNameButton.SetActive(true);
            UpdateButton();
        }
        else
        {
            clearNameButton.SetActive(false);
        }
    }

    private void UpdateButton()
    {
        if (nameInputText.text.Length > 0 || phoneInputText.text.Length > 0)
        {
            SaveButton.interactable = true;
        }
        else
        {
            SaveButton.interactable = false;
        }
    }

    public void OnChangePhoneInput()
    {
        if (phoneInputText.text.Length > 0)
        {
            clearPhoneButton.SetActive(true);
            UpdateButton();
        }
        else
        {
            clearPhoneButton.SetActive(false);
        }
    }

    public void SaveNewProfileInfo()
    {

        if (nameInputText.text != "")
        {
            currentUser.name = nameInputText.text;
        }
        else
        {
            currentUser.name = namePlaceHolder.text;
        }
        if (phoneInputText.text != "")
        {
            currentUser.related.phone = $"{countryValueText.text} {phoneInputText.text}";
        }
        else
        {
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
