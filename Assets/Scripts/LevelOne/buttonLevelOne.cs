using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonLevelOne : MonoBehaviour
{


    //the first drawer moving

    [SerializeField] GameObject drawerBottom, drawerMiddle, drawerTop;

    private bool draweBottomOpen, drawerMiddleOpen, drawerTopOpen = false;

    public void openDrawerOne()
    {
        if (!draweBottomOpen)
        {
            LeanTween.moveLocalX(drawerBottom, -0.7f, 1f).setEase(LeanTweenType.easeOutQuad);
            draweBottomOpen = true;
        }
        else
        {
            LeanTween.moveLocalX(drawerBottom, -0.2f, 1f).setEase(LeanTweenType.easeOutQuad);
            draweBottomOpen = false;
        }


    }


    public void openDrawerTwo()
    {
        if (!drawerMiddleOpen)
        {
            LeanTween.moveLocalX(drawerMiddle, -0.5f, 1f).setEase(LeanTweenType.easeOutQuad);
            drawerMiddleOpen = true;
        }
        else
        {
            LeanTween.moveLocalX(drawerMiddle, -0.2f, 1f).setEase(LeanTweenType.easeOutQuad);
            drawerMiddleOpen = false;
        }
    }

    public void openDrawerThree()
    {
        if (!drawerTopOpen)
        {
            LeanTween.moveLocalX(drawerTop, -0.3f, 1f).setEase(LeanTweenType.easeOutQuad);
            drawerTopOpen = true;
        }
        else
        {
            LeanTween.moveLocalX(drawerTop, -0.2f, 1f).setEase(LeanTweenType.easeOutQuad);
            drawerTopOpen = false;
        }
    }
}