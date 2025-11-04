using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


using System.Collections;

public class BunnyInterface : MonoBehaviour
{

    public List<Transform> pathPoints1;
    public List<Transform> pathPoints2;

    public GameObject parentObject;
    public bool loopAnimation = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.position = pathPoints1[0].position;
        Debug.Log("Starting Bunny Animation");
        transform.DOPath(pathPoints1.ConvertAll(p => p.position).ToArray(), 0.53f, PathType.CatmullRom, PathMode.TopDown2D).onComplete += () =>
        {
            StartCoroutine(waitForNextAnimation());
        };
    }

    public IEnumerator waitForNextAnimation()
    {
        yield return new WaitForSeconds(0.56f);
        NextAnimation();
    }

    public void NextAnimation()
    {
        transform.DOPath(pathPoints2.ConvertAll(p => p.position).ToArray(), 0.81f, PathType.CatmullRom, PathMode.TopDown2D).onComplete += () =>
        {
            if (loopAnimation)
            {
                Start();
            }
            else
            {
                parentObject.SetActive(false);
            }
        };
    }
    public void StopAnimation()
    {
        transform.DOKill();
    }

    public void StopNextCycle()
    {
        loopAnimation = false;
    }


}
