using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ApiManager : MonoBehaviour
{
    private static ApiManager _instance;

    public static ApiManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<ApiManager>();
            }
            return _instance;
        }
    }
    public string accessToken = "";
    private User currentUser;
    private static string BASE_API_URL = "https://yoyo-admin-bqaqewb0dhafbthh.canadacentral-01.azurewebsites.net/api/v1";
    private static string SIGNIN_ENDPOINT = BASE_API_URL + "/auth/signin";
    private static string LOGIN_ENDPOINT = BASE_API_URL + "/auth/login/";
    private static string RESET_PASSWORD_ENDPOINT = BASE_API_URL + "/auth/passwords/reset";
    private static string CHECK_ACCESS_CODE_ENDPOINT = BASE_API_URL + "/auth/access-codes/verification";
    private static string GET_REWARDS_ENDPOINT = BASE_API_URL + "/rewards";
    private static string GET_PLACES_ENDPOINT = BASE_API_URL + "/places";
    private static string DELETE_USER_ENDPOINT = BASE_API_URL + "/auth";
    private static string UPDATE_USER_ENDPOINT = BASE_API_URL + "/auth/info";

    public User GetUser()
    {
        return currentUser;
    }

    public void SetUser(User user)
    {
        currentUser = user;
        UpdateUserRequest updateData = new UpdateUserRequest
        {
            id = user.id.ToString(),
            name = user.name,
            age = user.related.age,
            gender = user.related.gender,
            phone = user.related.phone,
            pronouns = user.related.pronouns,
            taste_drink = user.related.taste_drink,
            taste_music = user.related.taste_music,
            taste_food = user.related.taste_food
        };
        UpdateUser(updateData, accessToken, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200)
            {
                UpdateUserResponse updateResponse = JsonUtility.FromJson<UpdateUserResponse>(responseText);
                Debug.Log($"User updated successfully: {updateResponse.user.name}");
            }
        });
    }

    public void SignIn(SignInRequest signInData, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(signInData);
        StartCoroutine(MakePostRequest(SIGNIN_ENDPOINT, jsonData, callback, false));
    }

    public void LogIn(LoginRequest loginData, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(loginData);
        StartCoroutine(MakePostRequest(LOGIN_ENDPOINT, jsonData, (response) =>
        {
            long responseCode = (long)response[0];
            string responseText = response[1].ToString();

            if (responseCode == 200)
            {
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);
                accessToken = loginResponse.access_token;
                currentUser = loginResponse.user;
            }
            callback(response);
        }, false));
    }

    public void ResetPassword(ResetPasswordRequest resetData, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(resetData);
        StartCoroutine(MakePostRequest(RESET_PASSWORD_ENDPOINT, jsonData, callback, false));
    }

    public void CheckAccessCode(CheckAccessCodeRequest accessCodeData, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(accessCodeData);
        StartCoroutine(MakePostRequest(CHECK_ACCESS_CODE_ENDPOINT, jsonData, callback, false));
    }

    public void GetRewards(int limit, int offset, string token, Action<object[]> callback)
    {
        string endpoint = $"{GET_REWARDS_ENDPOINT}/{limit}/{offset}";
        StartCoroutine(MakeGetRequest(endpoint, callback, token));
    }

    public void GetPlaces(int limit, int offset, string token, Action<object[]> callback)
    {
        string endpoint = $"{GET_PLACES_ENDPOINT}/{limit}/{offset}";
        StartCoroutine(MakeGetRequest(endpoint, callback, token));
    }

    public void DeleteUser(DeleteUserRequest deleteData, string token, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(deleteData);
        StartCoroutine(MakeDeleteRequest(DELETE_USER_ENDPOINT, jsonData, callback, token));
    }

    public void UpdateUser(UpdateUserRequest updateData, string token, Action<object[]> callback)
    {
        string jsonData = JsonUtility.ToJson(updateData);
        StartCoroutine(MakePutRequest(UPDATE_USER_ENDPOINT, jsonData, callback, token));
    }

    #region Private Helper Methods

    private string ConvertJsonToQueryParams(string jsonData)
    {
        if (string.IsNullOrEmpty(jsonData))
            return "";

        try
        {
            string cleanedJson = jsonData.Trim().TrimStart('{').TrimEnd('}');
            if (string.IsNullOrEmpty(cleanedJson))
                return "";

            var queryParams = new List<string>();
            string[] pairs = cleanedJson.Split(',');
            
            foreach (string pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.Trim()))
                    continue;
                    
                string[] keyValue = pair.Split(':');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim().Trim('"');
                    string value = keyValue[1].Trim().Trim('"');
                    
                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        string encodedKey = UnityEngine.Networking.UnityWebRequest.EscapeURL(key);
                        string encodedValue = UnityEngine.Networking.UnityWebRequest.EscapeURL(value);
                        queryParams.Add($"{encodedKey}={encodedValue}");
                    }
                }
            }

            return string.Join("&", queryParams);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error converting JSON to query params: {e.Message}");
            return "";
        }
    }

    private IEnumerator MakePostRequest(string endpoint, string jsonData, Action<object[]> callback, bool requiresAuth, string token = "")
    {
        using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(endpoint, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            if (requiresAuth && !string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            yield return request.SendWebRequest();

            HandleResponse(request, callback);
        }
    }

    private IEnumerator MakeGetRequest(string endpoint, Action<object[]> callback, string token = "")
    {
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(endpoint))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            yield return request.SendWebRequest();

            HandleResponse(request, callback);
        }
    }

    private IEnumerator MakeDeleteRequest(string endpoint, string jsonData, Action<object[]> callback, string token = "")
    {
        using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(endpoint, "DELETE"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            HandleResponse(request, callback);
        }
    }

    private IEnumerator MakePutRequest(string endpoint, string jsonData, Action<object[]> callback, string token = "")
    {

        string queryParams = ConvertJsonToQueryParams(jsonData);
        string fullEndpoint = string.IsNullOrEmpty(queryParams) ? endpoint : $"{endpoint}?{queryParams}";

        using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(fullEndpoint, "PUT"))
        {
            request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            HandleResponse(request, callback);
        }
    }

    private void HandleResponse(UnityEngine.Networking.UnityWebRequest request, Action<object[]> callback)
    {
        long responseCode = request.responseCode;

        if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log($"API Success - Code: {responseCode}, Response: {responseText}");
            callback(new object[] { responseCode, responseText });
        }
        else
        {
            string errorText = request.error;
            Debug.LogError($"API Error - Code: {responseCode}, Error: {errorText}");
            callback(new object[] { responseCode, errorText });
        }
    }

    #endregion
}
