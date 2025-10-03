using NUnit.Compatibility;
using System.Collections;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TMP_InputFieldScrollable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] ScrollRectFocusSettings m_scrollRectFocusSettings;
    public bool m_canStartCoroutine = true;

    private float m_maxTime = 0.25f;

    public void OnBeginDrag(PointerEventData eventData) {
        m_canStartCoroutine = false;
        m_scrollRectFocusSettings.scrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        m_canStartCoroutine = false;
        m_scrollRectFocusSettings.scrollRect.OnDrag(eventData); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_scrollRectFocusSettings.scrollRect.OnEndDrag(eventData);
        m_canStartCoroutine = true;
    }

    public void OnSelected()
    {
        m_scrollRectFocusSettings.OnSelectedInputText(GetComponent<RectTransform>());
    }

    public void OnDeselect()
    {
        StopAllCoroutines();
        m_canStartCoroutine = true;
        m_scrollRectFocusSettings.OnDeselect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_canStartCoroutine)
            StartCoroutine(CR_ClickTimer());
    }

    private IEnumerator CR_ClickTimer()
    {
        float time = 0f;

        while (time < m_maxTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        OnSelected();
    }
}