using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LevelThreeManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject escapeScreen;
    public GameObject healthBar;
    public GameObject escapeDoor;

    //spawning random enemies
    public GameObject[] enemies;
    public float interval = 5f;
    private float timer;
    public float yGenerateVal = 7f;
    public float minX = -20f; // Minimum X-axis position for generating game objects
    public float maxX = 20f; // Maximum X-axis position for generating game objects


    public Image healthBarGreen;
    public float healthAmount = 100f;
    public float damage = 30f;

    public Camera boyCamera;
    public Camera mousecamera;
    public Camera mainCamera;
    public int enemyNum;
    
    
    public bool isOver = false;

    private void Update()
    {
       
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if(enemyNum != 0)
            {
                generateObject();
                timer = interval;
            }
            
            
        }
    }
    private void Start()
    {
        deathScreen.SetActive(false);
        escapeScreen.SetActive(false);
        escapeDoor.SetActive(false);
        mousecamera.enabled = false;
        boyCamera.enabled = true;
        mainCamera.enabled = false;
        timer = interval;

    }

    public void takeDamage()
    {
        healthAmount -= damage;
        healthBarGreen.fillAmount = healthAmount / 100f;

        if(healthAmount <= 0)
        {
            isOver = true;
            deathSwitch(); //switch to death screen if ran out of health
        }
    }

    public void enemyDied()
    {
        enemyNum -= 1;
        soundManager.Instance.enemyDissapearSound();

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
        healthBar.SetActive(false);
        //deactivate health bar
    }


    public void escapeSwitch()
    {
        escapeScreen.SetActive(true);
        boyCamera.enabled = false;
        mainCamera.enabled = true;
        isOver = true;
        healthBar.SetActive(false);
        //deactivate health bar 
    }

    private void generateObject() { 

        GameObject prefab = enemies[Random.Range(0, enemies.Length)];
        float randomX = Random.Range(minX, maxX);

        Instantiate(prefab, new Vector2(randomX, yGenerateVal), Quaternion.identity);


    }
}
