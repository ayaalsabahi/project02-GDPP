using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{
    bool isPinFound = false;
    public GameObject pinButton;
    public GameObject doorToLift; 
    //create a 2d sprite thing that is initially off 
    private void Start()
    {
        pinButton.SetActive(false);
    }


    public void pinFound()
    {
        isPinFound = true;
        pinButton.SetActive(true);

    }

    public void exitedPin()
    {
        if (isPinFound)
        {
            LeanTween.moveLocalY(doorToLift, transform.localPosition.y + 0.1f, 2f)
            .setEase(LeanTweenType.easeOutQuad);
            //play door unlocking
            soundManager.Instance.doorSound();
        }
    }


}
