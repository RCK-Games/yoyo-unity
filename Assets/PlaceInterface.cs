using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlaceInterface : MonoBehaviour
{
    public TextMeshProUGUI titleText, descriptionText;

    public Image placeImage;

    public Place place;


    public void SetPlace(Place _place)
    {
        titleText.text = _place.name;
        place = _place;
        if (_place.media != null && _place.media.Count != 0)
        {
            ApiManager.instance.SetImageFromUrl(_place.media[0].absolute_url, (Sprite response) =>
            {
                placeImage.sprite = response;
            });
        }


    }

    public void OnClick()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.PlacesInfoViewModel, true);
        NewScreenManager.instance.GetCurrentView().GetComponent<PlacesInfoViewModel>().InitializeViewModel(place);
    }




}
