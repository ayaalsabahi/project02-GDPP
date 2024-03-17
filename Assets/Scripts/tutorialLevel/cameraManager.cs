using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    bool isDoll = true;
    public Camera dollCamera;
    public Camera possesionCamera;


   public void swicthCameras()
    {
        if (isDoll)
        {
            isDoll = false;
            dollCamera.enabled = false;
            possesionCamera.enabled = true;

        }
        else
        {
            isDoll = true;
            dollCamera.enabled = true;
            possesionCamera.enabled = false;

        }
    }
}
