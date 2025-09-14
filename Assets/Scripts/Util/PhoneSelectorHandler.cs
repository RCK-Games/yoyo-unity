using UnityEngine;
using TMPro;

public class PhoneSelectorHandler : MonoBehaviour
{
    public TextMeshProUGUI phoneInput, countryValue;
    public TMP_Dropdown countryDropdown;
    public string jsonData;
    public string countryCode;

    void Start()
    {
        CountriesWrapper countriesWrapper = JsonUtility.FromJson<CountriesWrapper>(jsonData);

        countryDropdown.options.Clear();
        foreach (var country in countriesWrapper.countries)
        {
            string optionText = $"{country.name} ({country.code})";
            countryDropdown.options.Add(new TMP_Dropdown.OptionData(optionText));
        }
        countryDropdown.value = 140;
    }

    public void SearchSpecificCountry(string searchText)
    {
        for (int i = 0; i < countryDropdown.options.Count; i++)
        {
            if (countryDropdown.options[i].text.ToLower().Contains(searchText.ToLower()))
            {
                countryDropdown.value = i;
                string[] stringParse = countryValue.text.Split('(');
                countryCode = stringParse[1].Replace(")", "").Trim();
                phoneInput.text = countryCode;
                break;
            }
        }
    }

    public void OnValueChanged_Country()
    {
        string[] stringParse = countryValue.text.Split('(');
        countryCode = stringParse[1].Replace(")", "").Trim();
        phoneInput.text = countryCode;
    }

    public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
}
[System.Serializable]
public class CountriesWrapper
{
    public CountryData[] countries;
}


[System.Serializable]
public class CountryData
{
    public string code;
    public string name;
}

