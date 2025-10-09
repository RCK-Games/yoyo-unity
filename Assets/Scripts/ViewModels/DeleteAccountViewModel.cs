using UnityEngine;
using TMPro;

public class DeleteAccountViewModel : ViewModel
{
    public TMP_InputField passwordInput;
    public TextMeshProUGUI errorText;

    public void OnClickShowPassword()
    {
        if (passwordInput.contentType == TMP_InputField.ContentType.Password)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.ForceLabelUpdate();
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            passwordInput.ForceLabelUpdate();
        }
    }

    public void OnDisable()
    {
        errorText.gameObject.SetActive(false);
        errorText.text = "";
        passwordInput.text = "";
    }

    public void OnClickDeleteAccount()
    {
        if (string.IsNullOrEmpty(passwordInput.text))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Please enter your password.";
            return;
        }

        DeleteUserRequest deleteRequest = new DeleteUserRequest
        {
            password = passwordInput.text
        };
        NewScreenManager.instance.ShowLoadingScreen(true);
        errorText.gameObject.SetActive(false);

        ApiManager.instance.DeleteUser(deleteRequest, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();
            NewScreenManager.instance.ShowLoadingScreen(false);

            if (responseCode == 200)
            {
                ApiManager.instance.accessToken = "";
                NewScreenManager.instance.ChangeToMainView(ViewID.WelcomeViewModel, false);
            }
            else
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "Incorrect password.";
            }
        });
    }
}
