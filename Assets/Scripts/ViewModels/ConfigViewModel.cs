using UnityEngine;
using DG.Tweening;

public class ConfigViewModel : ViewModel
{
    public GameObject configContainer, placeVM, rewardsVM;
    public RectTransform finalPosition, firstPosition;

    private string type;

    void OnEnable()
    {
        if (type == "places")
        {
            enableWithPlaces();
        }
        else if (type == "rewards")
        {
            enableWithRewards();
        }
    }

    void Start()
    {

    }


    public void enableWithRewards()
    {
        rewardsVM.SetActive(true);
        configContainer.transform.DOMoveY(finalPosition.transform.position.y, 0.5f);
        type = "rewards";
    }

    public void enableWithPlaces()
    {
        placeVM.SetActive(true);
        configContainer.transform.DOMoveY(finalPosition.transform.position.y, 0.5f);
        type = "places";
    }

    public void OnClickHide()
    {
        configContainer.transform.DOMoveY(firstPosition.transform.position.y, 0.5f).OnComplete(() =>
        {
            placeVM.SetActive(false);
            rewardsVM.SetActive(false);
            NewScreenManager.instance.BackToPreviousView();
            type = "";
        });
    }

    public void OnClickOpenProfile()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.ProfileViewModel, true);
    }
    public void OnClickLogOut()
    {
        ApiManager.instance.accessToken = "";
        NewScreenManager.instance.ChangeToMainView(ViewID.WelcomeViewModel, false);
    }
    public void OnClickOpenFAQ()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.FAQViewModel, true);
    }

    public void OnClickOpenTerms()
    {
        Application.OpenURL("https://github.com/");
    }
}
