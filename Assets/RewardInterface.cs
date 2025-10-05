using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class RewardInterface : MonoBehaviour
{
    public TextMeshProUGUI titleText, descriptionText;

    public Image placeImage;

    public Result result;


    public void SetPlace(Result _result)
    {
        titleText.text = _result.name;
        result = _result;
        if (_result.media != null && _result.media.Count != 0)
        {
            ApiManager.instance.SetImageFromUrl(_result.media[0].absolute_url, (Sprite response) =>
            {
                placeImage.sprite = response;
            });
        }


    }

    public void OnClick()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.PlacesInfoViewModel, true);
        NewScreenManager.instance.GetCurrentView().GetComponent<RewardsInfoViewModel>().InitializeViewModel(result);
    }




}
