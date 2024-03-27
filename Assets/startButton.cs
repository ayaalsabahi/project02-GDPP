using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Button pressed");
        SceneManager.LoadScene("tutorialLevel");
    }
}
