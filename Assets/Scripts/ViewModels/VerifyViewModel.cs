using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
public class VerifyViewModel : ViewModel
{
    public TMP_InputField codeInput;
    public GameObject showSuccessAnimation, showErrorText;

    public void OnClickCheckCode()
    {
        if (string.IsNullOrEmpty(codeInput.text))
        {
            return;
        }

        CheckAccessCodeRequest accessCodeData = new CheckAccessCodeRequest
        {
            code = codeInput.text
        };

        ApiManager.instance.CheckAccessCode(accessCodeData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200 || responseCode == 204)
            {
                NewScreenManager.instance.ChangeToMainView(ViewID.RegisterViewModel, true);
                showSuccessAnimation.SetActive(true);
                showErrorText.SetActive(false);
                return;
            }
            else if (responseCode == 404)
            {
                Debug.LogError("Access code not found");
            }
            else if (responseCode == 400)
            {
                Debug.LogError("Access code already redeemed");
            }
            else
            {
                Debug.LogError($"CheckAccessCode failed: {responseText}");
            }
            showErrorText.SetActive(true);
        });
    }
}
