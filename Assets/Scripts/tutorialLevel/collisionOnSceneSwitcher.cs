using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionOnSceneSwitcher : MonoBehaviour
{
    public Transform playerTrans;
    public float thresholdY = -5f;
    
    private void Update()
    {
        if(playerTrans.position.y < -5f)
        {
            SceneManager.LoadScene("LevelOne");
        }
    }
}
