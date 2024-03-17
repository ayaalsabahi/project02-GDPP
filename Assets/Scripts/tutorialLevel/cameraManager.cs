using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    bool isDoll = true;
    public Camera dollCamera;
    public Camera possesionCamera;

    private void Start()
    {
        dollCamera.enabled = true;
        possesionCamera.enabled = false;
    }

    public void swicthCameras()
    {
        if (!isDoll)
        {
            isDoll = true;
            dollCamera.enabled = false;
            possesionCamera.enabled = true;

        }
        else
        {
            isDoll = false;
            dollCamera.enabled = true;
            possesionCamera.enabled = false;

        }
    }
}
