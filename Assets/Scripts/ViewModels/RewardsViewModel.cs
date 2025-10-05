using UnityEngine;
using TMPro;
using System;
public class RewardsViewModel : ViewModel
{
    public TextMeshProUGUI pointsText;

    public GameObject placeItemPrefab, rewardsContainer, noRewardsIcon, rewardsLoadingIcon;

    public GameObject  partnersContainer, noPartnersText, partnersLoadingIcon;

    private bool gettingMoreRewards, gettingMorePartners;
    public Root rewardsResponse;
    public Root partnersResponse;

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
            ApiManager.instance.GetMoreRewards(rewardsResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    Root moreRewards = JsonUtility.FromJson<Root>(responseText);
                    rewardsResponse.next = moreRewards.next;
                    rewardsResponse.previous = moreRewards.previous;
                    rewardsResponse.results.AddRange(moreRewards.results);
                    GetRewardsCallback(moreRewards.results.ToArray());
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

        ApiManager.instance.GetRewards(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                rewardsResponse = JsonUtility.FromJson<Root>(responseText);

                GetRewardsCallback(rewardsResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetRewards failed: {responseText}");
            }
            
        });
    }
    private void GetRewardsCallback(Result[] results, bool isEvent = false)
    {
        if (rewardsResponse.total == 0)
        {
            noRewardsIcon.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject placeItem = Instantiate(placeItemPrefab, partnersContainer.transform);
            placeItem.GetComponent<RewardInterface>().SetPlace(item);
        }
        rewardsLoadingIcon.transform.SetAsLastSibling();
        gettingMoreRewards = false;
        rewardsLoadingIcon.SetActive(false);
    }

    private void GetPartners()
    {
        ApiManager.instance.GetPartners(10, 0, (object[] response) =>
        {

            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            if (responseCode == 200)
            {
                partnersResponse = JsonUtility.FromJson<Root>(responseText);

                GetPartnersCallback(partnersResponse.results.ToArray());
            }
            else
            {
                Debug.LogError($"GetPlaces failed: {responseText}");
            }
            
        });
    }

    private void GetPartnersCallback(Result[] results)
    {
        if (partnersResponse.total == 0)
        {
            noPartnersText.SetActive(true);
            return;
        }

        foreach (var item in results)
        {
            GameObject Item = Instantiate(placeItemPrefab, partnersContainer.transform);
            Item.GetComponent<RewardInterface>().SetPlace(item);
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
            ApiManager.instance.GetMorePartners(partnersResponse.next, (object[] response) =>
            {

                long responseCode = (long)response[0];
                string responseText = response[1].ToString();
                if (responseCode == 200)
                {
                    Root morePartners = JsonUtility.FromJson<Root>(responseText);
                    partnersResponse.next = morePartners.next;
                    partnersResponse.previous = morePartners.previous;
                    partnersResponse.results.AddRange(morePartners.results);
                    GetPartnersCallback(partnersResponse.results.ToArray());
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
