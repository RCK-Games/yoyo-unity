using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using System.Collections;
public class CardInterface : MonoBehaviour
{
    public GameObject cardFront, cardBack, contentValue, restPosition, cardPopUp;
    public TextMeshProUGUI pointsText, nameText, totalPointsText, idText;

    private Vector3 firstPosition;

    private float lastActivationTime = -1f;
    private float activationThreshold = 1f;

    private bool FirstAnimationDone, isAnimating;

    public bool isFullScreen;


    void OnEnable()
    {
        User user = ApiManager.instance.GetUser();
        if (user != null)
        {
            nameText.text = user.name;
            idText.text = user.id.ToString();
            totalPointsText.text = $"{user.related.total_points} POINTS";
            pointsText.text = $"{user.related.points} POINTS";
        }
        firstPosition = contentValue.transform.position;

        if (isFullScreen)
        {
            gameObject.transform.DOMove(restPosition.transform.position, 0.5f);
        }

        if (NewScreenManager.instance.GetCurrentView().GetComponent<PlacesViewModel>().GetCardValue())
        {
            FirstAnimationDone = true;
            cardFront.SetActive(false);
            cardBack.SetActive(true);
            cardBack.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            FirstAnimationDone = false;
            cardFront.SetActive(true);
            cardBack.SetActive(false);
            cardFront.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void OnDisable()
    {
        if (isFullScreen)
        {
            gameObject.transform.DOMove(firstPosition, 0.5f);
        }

    }

    public void OnClickClose(GameObject _gameObject)
    {
        gameObject.transform.DOMove(firstPosition, 0.5f).OnComplete(() =>
        {
            _gameObject.SetActive(false);
        });
        

    }

    public void OnClickAddPoints()
    {
        ApiManager.instance.GenerateWhatsAppMessage("Hola, me gustaría agregar puntos a mi cuenta.");
    }

    public void onDoubleTap()
    {
        float currentTime = Time.time;

        if (currentTime - lastActivationTime <= activationThreshold)
        {
            cardPopUp.SetActive(true);
        }

        lastActivationTime = currentTime;
    }

    public void OnValueChange(Vector2 value)
    {
        if (contentValue.transform.position.x == firstPosition.x)
        {
            return;
        }
        if (isAnimating)
        {
            return;
        }


        if (!FirstAnimationDone)
        {
            Debug.Log("1");
            PlayAnimationFirst();
            NewScreenManager.instance.GetCurrentView().GetComponent<PlacesViewModel>().SetCardValue(true);
        }
        else
        {
            Debug.Log("2");
            PlayAnimationSecond();
            NewScreenManager.instance.GetCurrentView().GetComponent<PlacesViewModel>().SetCardValue(false);
        }
        
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.5f);
        contentValue.transform.position = firstPosition;
            isAnimating = false;
    }

    public void PlayAnimationFirst()
    {
        isAnimating = true;
        cardFront.transform.DORotate(new Vector3(0, 90, 0), 0.5f).OnComplete(() => PlayAnimationFirstCallback());
    }

    private void PlayAnimationFirstCallback()
    {
        cardFront.SetActive(false);
        cardBack.transform.eulerAngles = new Vector3(0, 90, 0);
        cardBack.SetActive(true);
        cardBack.transform.DORotate(new Vector3(0, 180, 0), 0.5f).OnComplete(() =>
        {
            StartCoroutine(WaitAndReset());
            FirstAnimationDone = true;
        });
    }

    public void PlayAnimationSecond()
    {
        isAnimating = true;
        cardBack.transform.DORotate(new Vector3(0, 90, 0), 0.5f).OnComplete(()=>PlayAnimationSecondCallback());
    }

    private void PlayAnimationSecondCallback()
    {
        cardFront.SetActive(false);
        cardFront.SetActive(true);
        cardFront.transform.eulerAngles = new Vector3(0, 90, 0);
        cardFront.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        {
            StartCoroutine(WaitAndReset());
            FirstAnimationDone = false;
        });
    }
}
