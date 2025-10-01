using UnityEngine;
using System.Collections;

public class InputfieldSlideScreen : MonoBehaviour {

// Assign canvas here in editor
public Canvas canvas;

// Used internally - set by InputfieldFocused.cs
public bool InputFieldActive = false;
public RectTransform childRectTransform;


void LateUpdate () {
    if ((InputFieldActive)&&((TouchScreenKeyboard.visible)))
    {
    gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

    Vector3[ ] corners = {Vector3.zero,Vector3.zero,Vector3.zero,Vector3.zero};
    Rect rect = GetScreenRect(corners,childRectTransform);
    float keyboardHeight = TouchScreenKeyboard.area.height;

    float heightPercentOfKeyboard = keyboardHeight / (Screen.height * 100f);
    float heightPercentOfInput = (Screen.height-(rect.y+rect.height))/(Screen.height * 100f);

    if (heightPercentOfKeyboard>heightPercentOfInput)
    {
    // keyboard covers input field so move screen up to show keyboard
    float differenceHeightPercent = heightPercentOfKeyboard - heightPercentOfInput;
    float newYPos = gameObject.GetComponent<RectTransform>().rect.height /100f*differenceHeightPercent;

    Vector2 newAnchorPosition = Vector2.zero;
    newAnchorPosition.y = newYPos;
    gameObject.GetComponent<RectTransform>().anchoredPosition = newAnchorPosition;
    } else {
    // Keyboard top is below the position of the input field, so leave screen anchored at zero
    gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    } else {
    // No focus or touchscreen invisible, set screen anchor to zero
    gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    InputFieldActive = false;
}

public Rect GetScreenRect(Vector3[] corners,RectTransform rectTransform) {
    rectTransform.GetWorldCorners(corners);

    float xMin = float.PositiveInfinity, xMax = float.NegativeInfinity, yMin = float.PositiveInfinity, yMax = float.NegativeInfinity;
    for (int i = 0; i < 4; ++i) {
    // For Canvas mode Screen Space - Overlay there is no Camera; best solution I’ve found
    // is to use RectTransformUtility.WorldToScreenPoint) with a null camera.
    Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
    if (screenCoord.x < xMin) xMin = screenCoord.x;
    if (screenCoord.x > xMax) xMax = screenCoord.x;
    if (screenCoord.y < yMin) yMin = screenCoord.y;
    if (screenCoord.y > yMax) yMax = screenCoord.y;
    corners[i] = screenCoord;
    }
    Rect result = new Rect(xMin, Screen.height-yMin - (yMax - yMin), xMax - xMin, yMax - yMin);
    return result;
    }

}