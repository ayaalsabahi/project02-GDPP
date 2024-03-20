using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonEventsLevelOne : MonoBehaviour
{
    [Header("Events")]
    public GameEvent bottomDrawer;
    public GameEvent middleDrawer;
    public GameEvent topDrawer;
    public GameEvent switchPin;


    private void OnMouseDown()
    {
        string objectName = gameObject.name;
        switch (objectName)
        {
            case "bottomDrawerButton":
                bottomDrawer.Raise();
                break;
            case "middleDrawerButton":
                middleDrawer.Raise();
                break;
            case "topDrawerButton":
                topDrawer.Raise();
                break;
            case "pinPad":
                switchPin.Raise();
                break;
            case "exitButton":
                switchPin.Raise();
                Debug.Log("pin switched");
                break;
            case "doorExit":
                SceneManager.LoadScene("LevelTwo");
                break;
            default:
                Debug.Log("Unknown object clicked!");
                break;

        }
    }
}