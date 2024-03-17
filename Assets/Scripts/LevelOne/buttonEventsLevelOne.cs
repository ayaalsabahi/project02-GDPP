using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case "bottomDrawer":
                bottomDrawer.Raise();
                break;
            case "middleDrawer":
                middleDrawer.Raise();
                break;
            case "topDrawer":
                topDrawer.Raise();
                break;
            default:
                Debug.Log("Unknown object clicked!");
                break;

        }
    }
}