using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
public class VerifyViewModel : ViewModel
{
    public TMP_InputField codeInput;
    public GameObject showSuccessAnimation, showErrorText;
    public Button verifyButton;

    private string codeHandler;

    IEnumerator waitforSeconds()
    {
        showSuccessAnimation.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(2.25f);
        showSuccessAnimation.GetComponent<CanvasGroup>().DOFade(0, 1).OnComplete(() => showSuccessAnimation.SetActive(false));
        showErrorText.SetActive(false);
        NewScreenManager.instance.ChangeToMainView(ViewID.RegisterViewModel, true);
        NewScreenManager.instance.GetMainView(ViewID.RegisterViewModel).GetComponent<RegisterViewModel>().SetCode(codeHandler);
        codeInput.text = "";
    }

    public void OnClickShowPassword()
    {
        if (codeInput.contentType == TMP_InputField.ContentType.Password)
        {
            codeInput.contentType = TMP_InputField.ContentType.Standard;
            codeInput.ForceLabelUpdate();
        }
        else
        {
            codeInput.contentType = TMP_InputField.ContentType.Password;
            codeInput.ForceLabelUpdate();
        }
    }

    public void OnDisable()
    {
        showErrorText.SetActive(false);
        codeInput.text = "";
    }

    public void OnClickPaste()
    {
        if (GUIUtility.systemCopyBuffer != null && Regex.IsMatch(GUIUtility.systemCopyBuffer, @"^[a-zA-Z0-9]+$"))
        {
            codeInput.text = GUIUtility.systemCopyBuffer;
        }
        if (codeInput.text.Length == 6)
        {
            verifyButton.interactable = true;
        }
        else
        {
            verifyButton.interactable = false;
        }
    }

    public void OnValueChanged()
    {
        if (codeInput.text.Length == 6)
        {
            verifyButton.interactable = true;
        }
        else
        {
            verifyButton.interactable = false;
        }
    }

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
        NewScreenManager.instance.ShowLoadingScreen(true);

        ApiManager.instance.CheckAccessCode(accessCodeData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200 || responseCode == 204)
            {
                codeHandler = codeInput.text;
                showSuccessAnimation.SetActive(true);
                showSuccessAnimation.GetComponent<CanvasGroup>().alpha = 0;
                StartCoroutine(waitforSeconds());
                verifyButton.interactable = false;
                NewScreenManager.instance.ShowLoadingScreen(false);
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
            NewScreenManager.instance.ShowLoadingScreen(false);
            showErrorText.SetActive(true);
        });
    }
}
