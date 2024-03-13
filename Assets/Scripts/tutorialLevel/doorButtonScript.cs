using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorButtonScript : MonoBehaviour
{
    [Header("Events")]
    public GameEvent doorButtonPressed;

    private void OnMouseDown()
    {
        doorButtonPressed.Raise();
    }
}
