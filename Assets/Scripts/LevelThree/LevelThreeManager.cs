using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject escapeScreen;
    public Camera boyCamera;
    public Camera mousecamera;
    public int enemyNum;
    public GameObject escapeDoor; 

    private void Start()
    {
        deathScreen.SetActive(false);
        escapeScreen.SetActive(false);
        escapeDoor.SetActive(false);
        mousecamera.enabled = false;
        boyCamera.enabled = true;

    }

    public void enemyDied()
    {
        enemyNum -= 1;

        if (enemyNum == 0)
        {
            escapeDoor.SetActive(true);
            soundManager.Instance.keySound();
        }
    }

    //create a health decrease button
    //check if health reached 0 



    //create a death function button
    private void deathSwitch()
    {
        deathScreen.SetActive(true);
        boyCamera.enabled = false;
    }


    private void escapeSwitch()
    {
        escapeScreen.SetActive(true);
        boyCamera.enabled = false;
    }
}
