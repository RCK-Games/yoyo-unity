using UnityEngine;
using DG.Tweening;
public class MenuButtonInterface : MonoBehaviour
{
    public GameObject buttonIcon;
    public PlacesViewModel placesViewModel;

    public RewardsViewModel rewardsViewModel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleAnimation()
    {
        gameObject.transform.DOScale(0.8f, 0.3f);
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
        gameObject.transform.DOScale(1f, 0.3f);
        buttonIcon.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
    }
}
