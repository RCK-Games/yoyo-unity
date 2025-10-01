using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using DG.Tweening;
using System.Collections;
public class VerifyViewModel : ViewModel
{
    public TMP_InputField codeInput;
    public GameObject showSuccessAnimation, showErrorText;

    IEnumerator waitforSeconds()
    {
        yield return new WaitForSeconds(2);
        showSuccessAnimation.GetComponent<CanvasGroup>().DOFade(0, 1).OnComplete(() => showSuccessAnimation.SetActive(false));
        showErrorText.SetActive(false);
        NewScreenManager.instance.ChangeToMainView(ViewID.RegisterViewModel, true);
        NewScreenManager.instance.GetMainView(ViewID.RegisterViewModel).GetComponent<RegisterViewModel>().SetCode(codeInput.text);
    }

    public void OnClickPaste()
    {
        if (GUIUtility.systemCopyBuffer != null && Regex.IsMatch(GUIUtility.systemCopyBuffer, @"^[a-zA-Z0-9]+$"))
        {
            codeInput.text = GUIUtility.systemCopyBuffer;
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

        ApiManager.instance.CheckAccessCode(accessCodeData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200 || responseCode == 204)
            {

                showSuccessAnimation.SetActive(true);
                showSuccessAnimation.GetComponent<CanvasGroup>().alpha = 1;
                StartCoroutine(waitforSeconds());
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
