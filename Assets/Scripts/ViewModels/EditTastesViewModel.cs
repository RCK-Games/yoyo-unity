using UnityEngine;
using TMPro;
public class EditTastesViewModel : ViewModel
{

    public TMP_InputField drinkInputText, foodInputText, musicInputText;
    private User currentUser;
    public TextMeshProUGUI drinkPlaceHolder, foodPlaceHolder, musicPlaceHolder;

    public GameObject clearMUsicButton, clearFoodButton, clearDrinkButton;

    public void ClearDrinkInput()
    {
        drinkInputText.text = "";
        clearDrinkButton.SetActive(false);
    }

    public void ClearFoodInput()
    {
        foodInputText.text = "";
        clearFoodButton.SetActive(false);
    }

    public void ClearMusicInput()
    {
        musicInputText.text = "";
        clearMUsicButton.SetActive(false);
    }

    public void OnChangeDrinkInput()
    {
        if (drinkInputText.text.Length > 0)
        {
            clearDrinkButton.SetActive(true);
        }
        else
        {
            clearDrinkButton.SetActive(false);
        }
    }

    public void OnChangeFoodInput()
    {
        if (foodInputText.text.Length > 0)
        {
            clearFoodButton.SetActive(true);
        }
        else
        {
            clearFoodButton.SetActive(false);
        }
    }

    public void OnChangeMusicInput()
    {
        if (musicInputText.text.Length > 0)
        {
            clearMUsicButton.SetActive(true);
        }
        else
        {
            clearMUsicButton.SetActive(false);
        }
    }


    public void OnSetup(User _currentUser)
    {
        currentUser = _currentUser;
        drinkPlaceHolder.text = currentUser.related.taste_drink;
        foodPlaceHolder.text = currentUser.related.taste_food;
        musicPlaceHolder.text = currentUser.related.taste_music;
        if (drinkInputText.text.Length > 0)
        {
            clearDrinkButton.SetActive(true);
        }
        if (foodInputText.text.Length > 0)
        {
            clearFoodButton.SetActive(true);
        }
        if (musicInputText.text.Length > 0)
        {
            clearMUsicButton.SetActive(true);
        }
    }
    public void SaveNewTastes()
    {
        if (drinkInputText.text == "")
        {
            currentUser.related.taste_drink = drinkPlaceHolder.text;
        }
        else
        {
            currentUser.related.taste_drink = drinkInputText.text;
        }
        if( foodInputText.text == "")
        {
            currentUser.related.taste_food = foodPlaceHolder.text;
        }
        else
        {
            currentUser.related.taste_food = foodInputText.text;
        }
        if (musicInputText.text == "")
        {
            currentUser.related.taste_music = musicPlaceHolder.text;
        }
        else
        {
            currentUser.related.taste_music = musicInputText.text;
        }
        ApiManager.instance.SetUser(currentUser);
        NewScreenManager.instance.BackToPreviousView();
        NewScreenManager.instance.GetCurrentView().GetComponent<ProfileViewModel>().SetInfo();

    }
}
