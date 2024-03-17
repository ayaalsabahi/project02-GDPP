using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonLevelOne : MonoBehaviour
{


    //the first drawer moving

    [SerializeField] GameObject drawerBottom;

    private bool draweBottomOpen, drawerMiddleOpen, drawerTopOpen = false;

    public void openDrawerOne()
    {
        if (!draweBottomOpen)
        {
            LeanTween.moveX(drawerBottom, -0.00001f, 1f).setEase(LeanTweenType.easeOutQuad);
            draweBottomOpen = true;
        }
        else
        {
            LeanTween.moveX(drawerBottom, 2, 1f).setEase(LeanTweenType.easeOutQuad);
            draweBottomOpen = false;
        }
        
    }
}
