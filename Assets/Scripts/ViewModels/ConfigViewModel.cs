

public class ConfigViewModel : ViewModel
{


    public void OnClickOpenProfile()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.ProfileViewModel, true);
    }
    public void OnClickLogOut()
    {
        ApiManager.instance.accessToken = "";
        NewScreenManager.instance.ChangeToMainView(ViewID.LogInViewModel, true);
    }
    public void OnClickOpenFAQ()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.FAQViewModel, true);
    }
}
