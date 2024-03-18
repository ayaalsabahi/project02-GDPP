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
                SceneManager.LoadScene("pinpad");
                break;
            default:
                Debug.Log("Unknown object clicked!");
                break;

        }
    }
}