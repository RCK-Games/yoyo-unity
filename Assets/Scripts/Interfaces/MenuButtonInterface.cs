using UnityEngine;
using DG.Tweening;
public class MenuButtonInterface : MonoBehaviour
{
    public GameObject buttonIcon;
    public PlacesViewModel placesViewModel;

    public RewardsViewModel rewardsViewModel;

    public bool handlingAnimation = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleAnimation()
    {
        if(handlingAnimation)
        {
            return;
        }
        handlingAnimation = true;
        gameObject.transform.DOScale(0.8f, 0.3f).onComplete += () =>
        {
            gameObject.transform.DOScale(1f, 0.3f).onComplete += () =>
            {
                handlingAnimation = false;
            };
        };
        buttonIcon.transform.DORotate(new Vector3(0, 0, 45), 0.3f);
        if(placesViewModel != null)
        {
            placesViewModel.OnClickOpenConfig();
        }
        else
        {
            rewardsViewModel.OnClickOpenConfig();
        }
        
    }
    
    public void closeAnimation()
    {
        if(handlingAnimation)
        {
            return;
        }
        handlingAnimation = true;
        buttonIcon.transform.DORotate(new Vector3(0, 0, 0), 0.3f).onComplete += () =>
        {
            handlingAnimation = false;
        };
    }
}
