using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
public class RewadsInfoViewModel : ViewModel
{
    public TextMeshProUGUI titleText, descriptionText, validityText, conditionsText, costText, availableQuantityText;
    public RectTransform contentRebuild;
    public GameObject scrollSnapContainer, ImageGalleryContainer, ImageGalleryItemPrefab;


    /// <summary>
    /// Resets the view model when disabled to avoid data leakage between different places.

    void OnDisable()
    {
        scrollSnapContainer.SetActive(true);
        Destroy(scrollSnapContainer.GetComponent<SimpleScrollSnap>());
        titleText.text = "";
        descriptionText.text = "";
        validityText.text = "";
        conditionsText.text = "";
        costText.text = "";
        availableQuantityText.text = "";
        ImageGalleryContainer.SetActive(true);
        foreach (Transform child in ImageGalleryContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Initializes the view model with the provided place data.

    public void InitializeViewModel(Place _reward)
    {
        titleText.text = _reward.name;
        descriptionText.text = _reward.description;
        validityText.text = "valid";
        conditionsText.text = "_place.terms_and_conditions";
        costText.text = "_reward.cost.ToString()";
        availableQuantityText.text = "_reward.available_quantity.ToString()";

        scrollSnapContainer.AddComponent<SimpleScrollSnap>();

        if (_reward.gallery != null && _reward.gallery.Count > 0)
        {
            foreach (var media in _reward.gallery)
            {
                GameObject imageItem = Instantiate(ImageGalleryItemPrefab, ImageGalleryContainer.transform);
                ApiManager.instance.SetImageFromUrl(media.absolute_url, (Sprite response) =>
                {
                    imageItem.GetComponent<Image>().sprite = response;
                });
            }
        }
        else
        {
            if (_reward.media == null || _reward.media.Count == 0)
            {
                scrollSnapContainer.SetActive(false);
            }
            else
            {
                GameObject imageItem = Instantiate(ImageGalleryItemPrefab, ImageGalleryContainer.transform);
                ApiManager.instance.SetImageFromUrl(_reward.media[0].absolute_url, (Sprite response) =>
                {
                    imageItem.GetComponent<Image>().sprite = response;
                });
            }


        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRebuild);
        StartCoroutine(WaitAFrame());
    }
    
    IEnumerator WaitAFrame()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRebuild);
    }

}
