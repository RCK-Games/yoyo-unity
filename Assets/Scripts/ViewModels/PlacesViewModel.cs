using UnityEngine;
using TMPro;
using System;
public class PlacesViewModel : ViewModel
{
    public GameObject cardPopUpContainer;
    public GameObject placeItemPrefab, placesContainer, noPlacesText, placesLoadingIcon;

    public GameObject  eventsContainer, noEventsText, eventsLoadingIcon;

    private bool gettingMorePlaces, gettingMoreEvents;
    public PlacesResponse placesResponse;
    public PlacesResponse eventsResponse;

    private bool cardValue;

    void Start()
    {
        GetPlaces();
        GetEvents();
    }

    public void OnDoubleTap()
    {
        cardPopUpContainer.SetActive(true);
    }

    public void SetCardValue(bool value)
    {
        cardValue = value;
    }

    public bool GetCardValue()
    {
        return cardValue;
    }

    public void OnValueChangedPlacesSlider(Vector2 value)
    {
        if (value.x > 1.02f)
        {
            if (gettingMorePlaces) return;
            GetMorePlaces();
        }
    }

    public void OnValueChangedEventsSlider(Vector2 value)
    {
        if (value.x > 1.02f)
        {
            if(gettingMoreEvents) return;
            GetMoreEvents();
        }
    }

    public void ReloadAll()
    {
        ReloadPlaces();
        ReloadEvents();
    }

    public void ReloadPlaces()
    {
        foreach (Transform child in placesContainer.transform)
        {
            if (child.name == placeItemPrefab.name + "(Clone)")
            {
                GameObject.Destroy(child.gameObject);
            }
            
        }
        noPlacesText.SetActive(false);
        placesLoadingIcon.SetActive(true);
        GetPlaces();
    }

    public void ReloadEvents()
    {
        foreach (Transform child in eventsContainer.transform)
        {
            if (child.name == placeItemPrefab.name + "(Clone)")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        noEventsText.SetActive(false);
        eventsLoadingIcon.SetActive(true);
        GetEvents();
    }

    private void GetMorePlaces()
    {
        Debug.Log(placesResponse.next);
        if (placesResponse.next != null && placesResponse.next != "")
        {
            placesLoadingIcon.SetActive(true);
            gettingMorePlaces = true;
            ApiManager.instance.GetMorePlaces(placesResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    PlacesResponse morePlaces = JsonUtility.FromJson<PlacesResponse>(responseText);
                    placesResponse.next = morePlaces.next;
                    placesResponse.prev = morePlaces.prev;
                    placesResponse.results.AddRange(morePlaces.results);
                    GetPlacesCallback(morePlaces.results.ToArray());
                }
                else
                {
                    placesLoadingIcon.SetActive(false);
                    gettingMorePlaces = false;
                    Debug.LogError($"GetPlaces failed: {responseText}");
                }
            });
        }
    }

    private void GetPlaces()
    {
        ApiManager.instance.GetPlaces(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                placesResponse = JsonUtility.FromJson<PlacesResponse>(responseText);

                GetPlacesCallback(placesResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetRewards failed: {responseText}");
            }
            
        });
    }
    private void GetPlacesCallback(Place[] results, bool isEvent = false)
    {
        if (placesResponse.total == 0)
        {
            noPlacesText.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject placeItem = Instantiate(placeItemPrefab, placesContainer.transform);
            placeItem.GetComponent<PlaceInterface>().SetPlace(item, true);
        }
        placesLoadingIcon.transform.SetAsLastSibling();
        gettingMorePlaces = false;
        placesLoadingIcon.SetActive(false);
    }

    private void GetEvents()
    {
        ApiManager.instance.GetEvents(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                eventsResponse = JsonUtility.FromJson<PlacesResponse>(responseText);

                GetEventsCallback(eventsResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetPlaces failed: {responseText}");
            }
            
        });
    }

    private void GetEventsCallback(Place[] results)
    {
        if (eventsResponse.total == 0)
        {
            noEventsText.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject Item = Instantiate(placeItemPrefab, eventsContainer.transform);
            Item.GetComponent<PlaceInterface>().SetPlace(item, false);
        }
        eventsLoadingIcon.transform.SetAsLastSibling();
        gettingMoreEvents = false;
        eventsLoadingIcon.SetActive(false);
    }

    private void GetMoreEvents()
    {
        if (eventsResponse.next != null && eventsResponse.next != "")
        {
            eventsLoadingIcon.SetActive(true);
            gettingMoreEvents = true;
            ApiManager.instance.GetMorePlaces(eventsResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    PlacesResponse moreEvents = JsonUtility.FromJson<PlacesResponse>(responseText);
                    eventsResponse.next = moreEvents.next;
                    eventsResponse.prev = moreEvents.prev;
                    eventsResponse.results.AddRange(moreEvents.results);
                    GetEventsCallback(moreEvents.results.ToArray());
                }
                else
                {
                    eventsLoadingIcon.SetActive(false);
                    gettingMoreEvents = false;
                    Debug.LogError($"GetEvents failed: {responseText}");
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickOpenConfig()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.ConfigViewModel, true);
    }
}
