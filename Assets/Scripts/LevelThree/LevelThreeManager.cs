using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelThreeManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject escapeScreen;
    public GameObject healthBar;

    public Image healthBarGreen;
    public float healthAmount = 100f;
    public float damage = 30f;

    public Camera boyCamera;
    public Camera mousecamera;
    public Camera mainCamera;
    public int enemyNum;
    public GameObject escapeDoor;
    public bool isOver = false;


    private void Start()
    {
        deathScreen.SetActive(false);
        escapeScreen.SetActive(false);
        escapeDoor.SetActive(false);
        mousecamera.enabled = false;
        boyCamera.enabled = true;
        mainCamera.enabled = false;

    }

    public void takeDamage()
    {
        healthAmount -= damage;
        healthBarGreen.fillAmount = healthAmount / 100f;
    }

    public void enemyDied()
    {
        enemyNum -= 1;

        if (enemyNum == 0 && !isOver)
        {
            escapeDoor.SetActive(true); //after all enemies are avoided, door pops open
            soundManager.Instance.keySound();
        }
    }

    //create a health decrease button
    //check if health reached 0 



    //create a death function button
    public void deathSwitch()
    {
        deathScreen.SetActive(true);
        boyCamera.enabled = false;
        mainCamera.enabled = true;
        isOver = true;
        //deactivate health bar
    }


    public void escapeSwitch()
    {
        escapeScreen.SetActive(true);
        boyCamera.enabled = false;
        mainCamera.enabled = true;
        isOver = true;
        //deactivate health bar 
    }
}
