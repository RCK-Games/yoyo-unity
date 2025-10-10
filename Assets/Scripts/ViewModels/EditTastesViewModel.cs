using UnityEngine;
using TMPro;
public class EditTastesViewModel : ViewModel
{

    public TMP_InputField drinkInputText, foodInputText, musicInputText;
    private User currentUser;
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

    void OnDisable()
    {
        clearDrinkButton.SetActive(false);
        clearFoodButton.SetActive(false);
        clearMUsicButton.SetActive(false);
        drinkInputText.text = "";
        foodInputText.text = "";
        musicInputText.text = "";
        currentUser = null;
        
    }


    public void OnSetup(User _currentUser)
    {
        currentUser = _currentUser;
        drinkInputText.text = currentUser.related.taste_drink;
        foodInputText.text = currentUser.related.taste_food;
        musicInputText.text = currentUser.related.taste_music;
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
        currentUser.related.taste_drink = drinkInputText.text;
        currentUser.related.taste_food = foodInputText.text;
        currentUser.related.taste_music = musicInputText.text;
        ApiManager.instance.SetUser(currentUser);
        NewScreenManager.instance.BackToPreviousView();
        NewScreenManager.instance.GetCurrentView().GetComponent<ProfileViewModel>().SetInfo();

    }
}
