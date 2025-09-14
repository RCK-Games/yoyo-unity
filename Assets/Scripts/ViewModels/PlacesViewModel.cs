using UnityEngine;
using TMPro;
public class PlacesViewModel : ViewModel
{
    public TextMeshProUGUI pointsText, nameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        User user = ApiManager.instance.GetUser();
        if (user != null)
        {
            nameText.text = user.name;
            pointsText.text = $"{user.related.points} POINTS";
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
