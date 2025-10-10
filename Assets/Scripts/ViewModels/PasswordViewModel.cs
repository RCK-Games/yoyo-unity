using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
public class PasswordViewModel : ViewModel
{
    public TMP_InputField emailInput;
    public GameObject popUpSendMessage;

    public Button sendButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        emailInput.text = "";
        popUpSendMessage.SetActive(false);
    }

    public void OnChangeInput()
    {
        sendButton.interactable = IsValidEmail(emailInput.text);
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
    
    public void OnClickSendReset()
    {
        if (string.IsNullOrEmpty(emailInput.text))
        {
            return;
        }
        if (!IsValidEmail(emailInput.text))
        {
            return;
        }
        ResetPasswordRequest resetData = new ResetPasswordRequest
        {
            email = emailInput.text
        };

        ApiManager.instance.ResetPassword(resetData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 204)
            {
                popUpSendMessage.SetActive(true);
            }
            else
            {
                popUpSendMessage.SetActive(true);
            }
        });
    }
}
