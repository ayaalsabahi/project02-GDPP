using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionOnSceneSwitcher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the other object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Reached switching of scene");
            SceneManager.LoadScene("levelOne");
        }
    }
}
