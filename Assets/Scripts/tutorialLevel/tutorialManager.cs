using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    bool isDoll = true;
    int keyPressCount = 0;
    int doorPressCount = 0;
    bool isKeyCollected = false;

    //door rotation 
    public float rotationAngle = 70f; 
    public float duration = 1f;


    public GameObject keySprite;
    public GameObject doorSprite;
    public GameObject doorButton;
    public GameObject dissapearingGround;
    public GameObject colliderSceneSwitch; 

    [Header("Events")]
    public GameEvent keyPressedText;
    public GameEvent doorLockedText;
    public GameEvent tooSmallforDoor;

    private void Start()
    {
        doorButton.SetActive(false);

    }


    public void switchBodies()
    {
        isDoll = !isDoll;
    }

    
    public void addToKey()
    {
        //this means that the mouse is activated and the key can be collected
        if (!isDoll)
        {
            isKeyCollected = true;
            soundManager.Instance.keySound();
            keySprite.SetActive(false);
            doorButton.SetActive(true);
        }


        else
        {
            keyPressCount++;
            if (keyPressCount >= 3) keyPressedText.Raise();
        }

    }


    public void addToDoor()
    {
        if (isKeyCollected && isDoll)
        {
            LeanTween.rotateX(doorSprite, rotationAngle, duration).setEase(LeanTweenType.easeInOutQuad); //rotate door
            soundManager.Instance.doorSound();
            dissapearingGround.SetActive(false); //maybe make this fade out later on? 
            //Went through other scene 
        }

        //add a sound later on 
        else if (isKeyCollected && !isDoll)
        {
            tooSmallforDoor.Raise();
        } 
        else
        {
            doorPressCount++;
            if (doorPressCount >= 3) doorLockedText.Raise();
        }
           
    }

}
