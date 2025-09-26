using UnityEngine;
using TMPro;
using System;
public class RewardsViewModel : ViewModel
{
    public TextMeshProUGUI pointsText;

    public GameObject placeItemPrefab, rewardsContainer, noRewardsIcon, rewardsLoadingIcon;

    public GameObject  partnersContainer, noPartnersText, partnersLoadingIcon;

    private bool gettingMoreRewards, gettingMorePartners;
    public PlacesResponse rewardsResponse;
    public PlacesResponse partnersResponse;

    private bool cardValue;

    void Start()
    {
        User user = ApiManager.instance.GetUser();
        if (user != null)
        {
            pointsText.text = $"Available Points: {user.related.points}";
        }
        GetRewards();
        GetPartners();
    }



    public void OnValueChangedRewardsSlider(Vector2 value)
    {
        if (value.x > 1.02f)
        {
            if (gettingMoreRewards) return;
            GetMoreRewards();
        }
    }

    public void OnValueChangedPartnersSlider(Vector2 value)
    {
        if (value.x > 1.02f)
        {
            if(gettingMorePartners) return;
            GetMorePartners();
        }
    }

    private void GetMoreRewards()
    {
        Debug.Log(rewardsResponse.next);
        if (rewardsResponse.next != null && rewardsResponse.next != "")
        {
            rewardsLoadingIcon.SetActive(true);
            gettingMoreRewards = true;
            ApiManager.instance.GetMorePlaces(rewardsResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    PlacesResponse morePlaces = JsonUtility.FromJson<PlacesResponse>(responseText);
                    rewardsResponse.next = morePlaces.next;
                    rewardsResponse.prev = morePlaces.prev;
                    rewardsResponse.results.AddRange(morePlaces.results);
                    GetRewardsCallback(morePlaces.results.ToArray());
                }
                else
                {
                    rewardsLoadingIcon.SetActive(false);
                    gettingMoreRewards = false;
                    Debug.LogError($"GetPlaces failed: {responseText}");
                }
            });
        }
    }

    private void GetRewards()
    {

        //TODO: Cambiar a GetRewards
        ApiManager.instance.GetPlaces(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                rewardsResponse = JsonUtility.FromJson<PlacesResponse>(responseText);

                GetRewardsCallback(rewardsResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetRewards failed: {responseText}");
            }
            
        });
    }
    private void GetRewardsCallback(Place[] results, bool isEvent = false)
    {
        if (rewardsResponse.total == 0)
        {
            noRewardsIcon.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject placeItem = Instantiate(placeItemPrefab, partnersContainer.transform);
            placeItem.GetComponent<PlaceInterface>().SetPlace(item);
        }
        rewardsLoadingIcon.transform.SetAsLastSibling();
        gettingMoreRewards = false;
        rewardsLoadingIcon.SetActive(false);
    }

    private void GetPartners()
    {
        //Todo: Cambiar a GetPartners
        ApiManager.instance.GetEvents(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                partnersResponse = JsonUtility.FromJson<PlacesResponse>(responseText);

                GetPartnersCallback(partnersResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetPlaces failed: {responseText}");
            }
            
        });
    }

    private void GetPartnersCallback(Place[] results)
    {
        if (partnersResponse.total == 0)
        {
            noPartnersText.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject Item = Instantiate(placeItemPrefab, partnersContainer.transform);
            Item.GetComponent<PlaceInterface>().SetPlace(item);
        }
        partnersLoadingIcon.transform.SetAsLastSibling();
        gettingMorePartners = false;
        partnersLoadingIcon.SetActive(false);
    }

    private void GetMorePartners()
    {
        if (partnersResponse.next != null && partnersResponse.next != "")
        {
            partnersLoadingIcon.SetActive(true);
            gettingMorePartners = true;
            ApiManager.instance.GetMorePlaces(partnersResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    PlacesResponse moreEvents = JsonUtility.FromJson<PlacesResponse>(responseText);
                    partnersResponse.next = moreEvents.next;
                    partnersResponse.prev = moreEvents.prev;
                    partnersResponse.results.AddRange(moreEvents.results);
                    GetPartnersCallback(moreEvents.results.ToArray());
                }
                else
                {
                    partnersLoadingIcon.SetActive(false);
                    gettingMorePartners = false;
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
