using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
public class RewardsInfoViewModel : ViewModel
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

    public void OnClickRedeem()
    {
        // Implement the redeem functionality here
        Debug.Log("Redeem button clicked for: " + titleText.text);

    }

    /// <summary>
    /// Initializes the view model with the provided place data.

    public void InitializeViewModel(Result _reward)
    {
        titleText.text = _reward.name;
        descriptionText.text = _reward.description;
        validityText.text = _reward.starts_on + " - " + _reward.ends_on;
        conditionsText.text = _reward.conditions;
        costText.text = _reward.cost.ToString();
        availableQuantityText.text = _reward.stock.ToString();

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
