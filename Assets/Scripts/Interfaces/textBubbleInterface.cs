using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textBubbleInterface : MonoBehaviour
{
    public string TextToShow;
    public GameObject textLocation;
    public GameObject textBubblePrefab;
    public GameObject textBubbleContainer;
    public GameObject textBubbleCloseButton;


    public void OnClickCreateTextBubble()
    {
        GameObject newTextBubble = Instantiate(textBubblePrefab, textBubbleContainer.transform);
        newTextBubble.GetComponentInChildren<TextMeshProUGUI>().text = TextToShow;
        newTextBubble.transform.position = textLocation.transform.position;
        textBubbleCloseButton.SetActive(true);
        textBubbleCloseButton.GetComponent<Button>().onClick.AddListener(() => { Destroy(newTextBubble); textBubbleCloseButton.SetActive(false); });
    }
}
