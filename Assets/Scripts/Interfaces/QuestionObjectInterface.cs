using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class QuestionObjectInterface : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject questionContent, arrowIcon;
    public RectTransform contentParent;

    public void ToggleQuestion()
    {
        if (isOpen)
        {
            questionContent.SetActive(false);
            isOpen = false;
            arrowIcon.transform.DORotate(new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            questionContent.SetActive(true);
            isOpen = true;
            arrowIcon.transform.DORotate(new Vector3(0, 0, -90), 0.3f);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
    }

    void OnDisable()
    {
        isOpen = false;
        questionContent.SetActive(false);
        arrowIcon.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
