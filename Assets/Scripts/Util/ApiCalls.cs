using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiCalls
{
    public const string ENDPOINT_URL = "https://www.google.com/api/v1/";

    public static IEnumerator CR_SignIn(
        string _name,
        string _email,
        string _password,
        int _age,
        string _gender,
        string _phone,
        int _points,
        string _access_code,
        bool _related,
        Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + "auth/signin";

        SignInJSONBody signInJSONBody = new SignInJSONBody
        {
            name = _name,
            email = _email,
            password = _password,
            age = _age,
            gender = _gender,
            phone = _phone,
            points = _points,
            access_code = _access_code,
            related = _related,
        };

        string jsonRaw = JsonUtility.ToJson(signInJSONBody);

        DebugLogManager.instance.DebugLog($"Body request for creating a client user is: {jsonRaw}");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonRaw, "application/json"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                while (!webRequest.isDone) { yield return null; }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error on sign in client user. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog($"Create client user {jsonResult}");
                    ClientUser clientUser = JsonUtility.FromJson<ClientUser>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, clientUser });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on create client user {jsonResult}");
            yield break;
        }
    }

    public static IEnumerator CR_LogIn(
        string _email,
        string _password,
        Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + "auth/login";

        LogInJSONBody logInJSONBody = new LogInJSONBody
        {
            email = _email,
            password = _password,
        };

        string jsonRaw = JsonUtility.ToJson(logInJSONBody);

        DebugLogManager.instance.DebugLog($"Body request for log in a client user is: {jsonRaw}");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonRaw, "application/json"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                if (webRequest.responseCode == WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE)
                {
                    while (!webRequest.isDone) { yield return null; }

                    jsonResult = webRequest.downloadHandler.text;
                    ErrorMessageResponseJSON errorMessageResponseJSON = JsonUtility.FromJson<ErrorMessageResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, errorMessageResponseJSON.message });
                    yield break;
                }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error on log in client user. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog($"Logged in client user {jsonResult}");
                    LogInResponseJSON logInResponseJSONBody = JsonUtility.FromJson<LogInResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, logInResponseJSONBody.user, logInResponseJSONBody.token });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on log in client user {jsonResult}");
            yield break;
        }
    }

    public static IEnumerator CR_ResetPassword(
        string _email,
        Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + "auth/login";

        ResetPasswordJSONBody resetPasswordJSONBody = new ResetPasswordJSONBody
        {
            email = _email,
        };

        string jsonRaw = JsonUtility.ToJson(resetPasswordJSONBody);

        DebugLogManager.instance.DebugLog($"Body request to reset password is: {jsonRaw}");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonRaw, "application/json"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                while (!webRequest.isDone) { yield return null; }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error on reset password. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    _callback(new object[] { webRequest.responseCode });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on reset password {jsonResult}");
            yield break;
        }
    }

    public static IEnumerator CR_CheckAccessCode(
        string _code,
        Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + "codes/exchange";

        CheckAccessCodeJSONBody checkAccessCodeJSONBody = new CheckAccessCodeJSONBody
        {
            code = _code
        };

        string jsonRaw = JsonUtility.ToJson(checkAccessCodeJSONBody);

        DebugLogManager.instance.DebugLog($"Body request for a code exchange is: {jsonRaw}");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonRaw, "application/json"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.responseCode == WebCallsUtils.ERROR_RESPONSE_CODE || webRequest.responseCode == WebCallsUtils.NOT_FOUND_RESPONSE_CODE)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    ErrorMessageResponseJSON errorMessageResponseJSON = JsonUtility.FromJson<ErrorMessageResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, errorMessageResponseJSON.message });
                    yield break;
                }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error on check access code. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog($"On check access cod {jsonResult}");
                    CheckAccessCodeResponseJSON checkAccessCodeResponseJSON = JsonUtility.FromJson<CheckAccessCodeResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, checkAccessCodeResponseJSON.valid });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on check access code {jsonResult}");
            yield break;
        }
    }

    public static IEnumerator CR_GetRewards(string _token,int _limit, int _offset, Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + $"rewards/{_limit}/{_offset}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                while (!webRequest.isDone) { yield return null; }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error get rewards. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog("Fetch rewards result: " + jsonResult);
                    GetRewardsResponseJSON getRewardsResponseJSON = JsonUtility.FromJson<GetRewardsResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, getRewardsResponseJSON.rewards, getRewardsResponseJSON.prev, getRewardsResponseJSON.next });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on fetch rewards: {jsonResult}");
            yield break;

        }
    }

    public static IEnumerator CR_GetPlaces(string _token, int _limit, int _offset, Action<object[]> _callback)
    {
        string jsonResult = "";

        string url = ENDPOINT_URL + $"places/{_limit}/{_offset}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                while (!webRequest.isDone) { yield return null; }

                _callback(new object[] { webRequest.responseCode, null });
                DebugLogManager.instance.DebugLog($"Protocol Error or Connection Error get places. Response Code: {webRequest.responseCode}. Result: {webRequest.result.ToString()} ");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    DebugLogManager.instance.DebugLog("Fetch places result: " + jsonResult);
                    GetPlacesResponseJSON getPlacesResponseJSON = JsonUtility.FromJson<GetPlacesResponseJSON>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, getPlacesResponseJSON.places, getPlacesResponseJSON.prev, getPlacesResponseJSON.next });
                    yield break;
                }
            }

            DebugLogManager.instance.DebugLog($"Failed on fetch places: {jsonResult}");
            yield break;

        }
    }
}
