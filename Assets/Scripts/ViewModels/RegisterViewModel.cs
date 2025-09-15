using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class RegisterViewModel : ViewModel
{
    public TMP_InputField emailInput, passwordInput, confirmPasswordInput, pronounsInput, ageInput, phoneInput, nameInput, accessCodeInput;
    public Boolean termsAccepted = false;
    public TextMeshProUGUI countryCode;
    public string Gender;
    public GameObject errorMessage, passwordErrorMessage, termsAcceptedGraphic;

    public ScrollRect scrollRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValueChanged_Gender(int index)
    {
        switch (index)
        {
            case 0:
                Gender = "Women";
                break;
            case 1:
                Gender = "Men";
                break;
            case 2:
                Gender = "I prefer not to say it";
                break;
        }
    }

    public void OnValueChanged_TermsAccepted(bool value)
    {
        termsAccepted = value;
        termsAcceptedGraphic.SetActive(value);
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    public void OnClickRegister()
    {
        if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrEmpty(passwordInput.text) || string.IsNullOrEmpty(confirmPasswordInput.text)
            || string.IsNullOrEmpty(pronounsInput.text) || string.IsNullOrEmpty(ageInput.text) || string.IsNullOrEmpty(phoneInput.text)
            || string.IsNullOrEmpty(nameInput.text) || string.IsNullOrEmpty(accessCodeInput.text))
        {
            errorMessage.SetActive(true);
            scrollRect.normalizedPosition = new Vector2(0, 0);
            return;
        }

        if (!IsValidEmail(emailInput.text))
        {
            errorMessage.SetActive(true);
            scrollRect.normalizedPosition = new Vector2(0, 0);
            return;
        }

        if (!termsAccepted)
        {
            errorMessage.SetActive(true);
            scrollRect.normalizedPosition = new Vector2(0, 0);
            return;
        }

        if (passwordInput.text != confirmPasswordInput.text)
        {
            passwordErrorMessage.SetActive(true);
            scrollRect.normalizedPosition = new Vector2(0, 0);
            return;
        }

        SignInRequest signInData = new SignInRequest
        {
            name = nameInput.text,
            email = emailInput.text,
            age = int.Parse(ageInput.text),
            gender = Gender,
            phone = countryCode.text + " " + phoneInput.text,
            password = passwordInput.text,
            points = 0,
            pronouns = pronounsInput.text,
            access_code = accessCodeInput.text
        };
        
        Debug.Log(signInData.phone);

        passwordErrorMessage.SetActive(false);
        errorMessage.SetActive(false);

        ApiManager.instance.SignIn(signInData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200)
            {
                NewScreenManager.instance.ChangeToMainView(ViewID.LogInViewModel, true);
                NewScreenManager.instance.GetMainView(ViewID.LogInViewModel).GetComponent<LogInViewModel>().showValidateEmailMessage();

            }
            else if (responseCode == 401)
            {
                ErrorResponse errorResponse = JsonUtility.FromJson<ErrorResponse>(responseText);
                Debug.LogError($"SignIn failed: {errorResponse.error_code}");
                errorMessage.SetActive(true);
            }
            else
            {
                Debug.LogError($"SignIn failed: {responseText}");
                errorMessage.SetActive(true);
            }
        });
        NewScreenManager.instance.ChangeToMainView(ViewID.RegisterViewModel, true);
    }
}
