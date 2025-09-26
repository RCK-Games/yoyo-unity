using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using System.Collections;
public class BottomInterface : MonoBehaviour
{

    public void OnClickGoToPlaces()
    {
        if (NewScreenManager.instance.GetCurrentView().viewID == ViewID.PlacesViewModel)
        {
            return;
        }
        else
        {
            NewScreenManager.instance.BackToPreviousView();
        }
    }

    public void OnClickGoToRewards()
    {
        if (NewScreenManager.instance.GetCurrentView().viewID == ViewID.RewardsViewModel)
        {
            return;
        }
        else
        {
            NewScreenManager.instance.ChangeToMainView(ViewID.RewardsViewModel, true);
        }
    }

}
