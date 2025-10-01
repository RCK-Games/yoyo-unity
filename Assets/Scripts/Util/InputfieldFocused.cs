using UnityEngine;
using TMPro;
public class InputfieldFocused : MonoBehaviour {

InputfieldSlideScreen slideScreen;
TextMeshProUGUI inputField;

    void Start() {
        slideScreen = GameObject.FindFirstObjectByType<InputfieldSlideScreen>();
        inputField = gameObject.GetComponent<TextMeshProUGUI>();
    }



    public void OnSelectInputField() {
        slideScreen.InputFieldActive = true;
        slideScreen.childRectTransform = transform.GetComponent<RectTransform>();
    }
}