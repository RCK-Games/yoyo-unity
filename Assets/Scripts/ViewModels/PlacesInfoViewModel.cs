using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
public class PlacesInfoViewModel : ViewModel
{
    public TextMeshProUGUI titleText, descriptionText, musicLineUpText, locationText;

    public GameObject musicLineUpContainer, locationContainer, timeContainer, TagsContainer, tagParent, tagItemPrefab, FacebookButton, InstagramButton, WebsiteButton;
    public GameObject costRateContainer, paymentOptionsContainer, dressCodeContainer, socialMediaContainer, timeTextPrefab, timeIcon, timeTextContainer;
    public RectTransform contentRebuild;
    public GameObject ImageGalleryContainer, ImageGalleryItemPrefab, scrollSnapContainer, dressCodeSpacer;

    public List<GameObject> costRate = new List<GameObject>();
    public List<GameObject> paymentOptions = new List<GameObject>();
    public List<GameObject> dressCodeOptions = new List<GameObject>();

    /// <summary>
    /// Resets the view model when disabled to avoid data leakage between different places.

    void OnDisable()
    {
        scrollSnapContainer.SetActive(true);
        Destroy(scrollSnapContainer.GetComponent<SimpleScrollSnap>());
        titleText.text = "";
        descriptionText.text = "";
        WebsiteButton.SetActive(true);
        FacebookButton.SetActive(true);
        InstagramButton.SetActive(true);
        WebsiteButton.GetComponent<Button>().onClick.RemoveAllListeners();
        FacebookButton.GetComponent<Button>().onClick.RemoveAllListeners();
        InstagramButton.GetComponent<Button>().onClick.RemoveAllListeners();
        musicLineUpContainer.SetActive(true);
        musicLineUpText.text = "";
        locationContainer.SetActive(true);
        locationText.text = "";
        timeContainer.SetActive(true);
        foreach (Transform child in TagsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        paymentOptionsContainer.SetActive(true);
        foreach (var option in paymentOptions)
        {
            option.SetActive(false);
        }
        costRateContainer.SetActive(true);
        foreach (var item in costRate)
        {
            item.SetActive(false);
        }
        dressCodeContainer.SetActive(true);
        tagParent.SetActive(true);
        paymentOptionsContainer.SetActive(true);
        socialMediaContainer.SetActive(true);
        costRateContainer.SetActive(true);
        dressCodeOptions[0].SetActive(false);
        dressCodeOptions[1].SetActive(false);

        ImageGalleryContainer.SetActive(true);

        foreach (Transform child in timeTextContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        foreach (Transform child in ImageGalleryContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }

    /// <summary>
    /// Initializes the view model with the provided place data.

    public void InitializeViewModel(Place _place)
    {
        titleText.text = _place.name;
        descriptionText.text = _place.description;

        if (_place.music_genre_list != null && _place.music_genre_list.Count > 0)
        {
            if (_place.music_genre_list[0] != "" && _place.music_genre_list.Count != 1)
            {
                foreach (var genre in _place.music_genre_list)
                {
                    GameObject tag = Instantiate(tagItemPrefab, TagsContainer.transform);
                    tag.GetComponentInChildren<TextMeshProUGUI>().text = genre;
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(TagsContainer.GetComponent<RectTransform>());
            }
            else
            {
                tagParent.SetActive(false);
            }
        }
        else
        {
            tagParent.SetActive(false);
        }

        scrollSnapContainer.AddComponent<SimpleScrollSnap>();

        if (_place.gallery != null && _place.gallery.Count > 0)
        {
            foreach (var media in _place.gallery)
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
            if (_place.media == null || _place.media.Count == 0)
            {
                scrollSnapContainer.SetActive(false);
            }
            else
            {
                GameObject imageItem = Instantiate(ImageGalleryItemPrefab, ImageGalleryContainer.transform);
                ApiManager.instance.SetImageFromUrl(_place.media[0].absolute_url, (Sprite response) =>
                {
                    imageItem.GetComponent<Image>().sprite = response;
                });
            }


        }

        if (_place.payment_options_list.Count == 0 || _place.payment_options_list[0] == "")
        {
            paymentOptionsContainer.SetActive(false);
        }
        else
        {
            for (int i = 0; i < _place.payment_options_list.Count; i++)
            {
                if (_place.payment_options_list[i].ToLower().Contains("cash"))
                {
                    paymentOptions[0].SetActive(true);
                }
                if (_place.payment_options_list[i].ToLower().Contains("card"))
                {
                    paymentOptions[1].SetActive(true);
                }

            }
        }

        if (_place.schedule_list.Count == 0 || _place.schedule_list[0] == "")
        {
            timeContainer.SetActive(false);
        }
        else
        {
            for (int i = 0; i < _place.schedule_list.Count; i++)
            {
                GameObject timeItem = Instantiate(timeTextPrefab, timeTextContainer.transform);
                timeItem.GetComponent<TextMeshProUGUI>().text = _place.schedule_list[i];
            }
            timeIcon.transform.position = new Vector2(timeIcon.transform.position.x, timeContainer.transform.position.y);
        }


        if (_place.dresscode == null || _place.dresscode == "")
        {
            dressCodeContainer.SetActive(false);
        }
        else
        {
            if (_place.dresscode.ToLower().Contains("formal"))
            {
                dressCodeOptions[0].SetActive(true);
            }
            if (_place.dresscode.ToLower().Contains("casual"))
            {
                dressCodeOptions[1].SetActive(true);
            }

        }


        if (_place.cost_rate == null || _place.cost_rate == "")
        {
            costRateContainer.SetActive(false);
        }



        if (_place.website_url == "")
        {
            WebsiteButton.SetActive(false);
        }
        else
        {
            WebsiteButton.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(_place.website_url));
        }

        if (_place.facebook_url == "")
        {
            FacebookButton.SetActive(false);
        }
        else
        {
            FacebookButton.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(_place.facebook_url));
        }

        if (_place.instagram_url == "")
        {
            InstagramButton.SetActive(false);
        }
        else
        {
            InstagramButton.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(_place.instagram_url));
        }

        if (WebsiteButton.activeSelf == false && FacebookButton.activeSelf == false && InstagramButton.activeSelf == false)
        {
            socialMediaContainer.SetActive(false);
        }


        if (_place.music_lineup == "")
        {
            musicLineUpContainer.SetActive(false);
        }
        else
        {
            musicLineUpText.text = _place.music_lineup;
        }
        if (_place.address == "")
        {
            locationContainer.SetActive(false);
        }
        else
        {
            locationText.text = _place.address;
        }
        if (_place.cost_rate != null && _place.cost_rate != "")
        {

            for (int i = 0; i < costRate.Count; i++)
            {
                if (i < _place.cost_rate.Length)
                {
                    costRate[i].SetActive(true);
                }
                else
                {
                    costRate[i].SetActive(false);
                }
            }
        }
        else
        {
            foreach (var item in costRate)
            {
                item.SetActive(false);
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
