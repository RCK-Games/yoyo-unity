using UnityEngine;

public class WelcomeViewModel : ViewModel
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickLogIn()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.LogInViewModel , true);
    }
    
    public void OnClickRegister()
    {
        NewScreenManager.instance.ChangeToMainView(ViewID.VerifyViewModel , true);
    }
}
