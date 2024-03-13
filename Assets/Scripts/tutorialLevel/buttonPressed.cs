using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPressed : MonoBehaviour
{
    [Header("Events")]
    public GameEvent keyButtonPressed;

    private void OnMouseDown()
    {
        keyButtonPressed.Raise();
    }
}
