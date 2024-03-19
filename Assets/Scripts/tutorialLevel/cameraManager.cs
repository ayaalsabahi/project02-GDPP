using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    bool isDoll = true;

    bool isPin = false;

    public Camera dollCamera;
    public Camera possesionCamera;
    public Camera pinCamera;


    public GameObject inputPin; //text input box

    private void Start()
    {
        dollCamera.enabled = true;
        possesionCamera.enabled = false;
        inputPin.SetActive(false);
    }

    //toggle between the mouse and boy camera 
    public void swicthCameras()
    {
        if (!isDoll)
        {
            isDoll = true;
            dollCamera.enabled = true;
            possesionCamera.enabled = false;

        }
        else
        {
            isDoll = false;
            dollCamera.enabled = false;
            possesionCamera.enabled = true;

        }
    }

    //toggle between the character and pin camera upon exit 
    public void switchPinCamera()
    {
       

        if (isPin)
        {
            isPin = false; //swicth out of the pin scene
            pinCamera.enabled = false;
            pinCamera.enabled = false;
            if (isDoll) dollCamera.enabled = true;
            else possesionCamera.enabled = true;
            inputPin.SetActive(false);


        }
        else
        {
            isPin = true; //formerly was at the pin scene
            pinCamera.enabled = true;
            dollCamera.enabled = false;
            possesionCamera.enabled = false;
            inputPin.SetActive(true);

        }
    }
}
