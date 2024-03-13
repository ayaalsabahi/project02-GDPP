using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    bool isDoll = true;
    int keyPressCount = 0;
    int doorPressCount = 0;
    bool isKeyCollected = false;


    [Header("Events")]
    public GameEvent keyPressedText;

    public GameEvent doorLockedText;
    //public GameEvent doorUnlocked;

    //add a listener for when the body is switched event
    public void switchBodies()
    {
        isDoll = !isDoll;
    }

    
    public void addToKey()
    {
        //this means that the mouse is activated and the key can be collected
        if (!isDoll) Debug.Log("Key was collected"); //link to sound manager possibly
        //also link to UI elements such as the key dissapearing and popping up on screen

        else
        {
            keyPressCount++;
            if (keyPressCount == 3) keyPressedText.Raise();
        }

    }


    public void addToDoor()
    {
        if (isKeyCollected && isDoll) Debug.Log("Can now go through to other scene"); //add a sound later on 
        else if (isKeyCollected && !isDoll) Debug.Log("Too small to reach handle");
        else
        {
            doorPressCount++;
            if (doorPressCount == 3) doorLockedText.Raise();
        }
           
    }

}
