using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EditProfileViewModel : ViewModel
{
    public TMP_InputField nameInputText, phoneInputText;
    private User currentUser;
    public TextMeshProUGUI countryValueText;
    public PhoneSelectorHandler phoneSelectorHandler;
    public GameObject clearNameButton, clearPhoneButton;

    public Button SaveButton;
    public void OnSetup(User _currentUser)
    {
        currentUser = _currentUser;
        nameInputText.text = currentUser.name;

        if (currentUser.related.phone.Length > 0)
        {
            phoneInputText.text = currentUser.related.phone.Split(' ')[1];
            phoneSelectorHandler.SearchSpecificCountry(currentUser.related.phone.Split(' ')[0].Replace("(", "").Replace(")", "").Replace("+", ""));
        }
    }

    void OnDisable()
    {
        nameInputText.text = "";
        phoneInputText.text = "";
        clearNameButton.SetActive(true);
        clearPhoneButton.SetActive(true);
        SaveButton.interactable = true;
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
        if (nameInputText.text.Length > 0 || phoneInputText.text.Length > 9)
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
        if (phoneInputText.text.Length > 9)
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
            return;
        }
        if (phoneInputText.text != "")
        {
            currentUser.related.phone = $"{countryValueText.text} {phoneInputText.text}";
        }
        else
        {
            return;
        }

        NewScreenManager.instance.BackToPreviousView();
        ApiManager.instance.SetUser(currentUser);
        NewScreenManager.instance.GetCurrentView().GetComponent<ProfileViewModel>().SetInfo();
    }
}
