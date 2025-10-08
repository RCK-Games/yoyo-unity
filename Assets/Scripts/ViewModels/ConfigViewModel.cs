using UnityEngine;

public class ConfigViewModel : ViewModel
{


    public void OnClickOpenProfile()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.ProfileViewModel, true);
    }
    public void OnClickLogOut()
    {
        ApiManager.instance.accessToken = "";
        NewScreenManager.instance.ChangeToMainView(ViewID.WelcomeViewModel, false);
    }
    public void OnClickOpenFAQ()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.FAQViewModel, true);
    }

    public void OnClickOpenTerms()
    {
        Application.OpenURL("https://github.com/");
    }
}
