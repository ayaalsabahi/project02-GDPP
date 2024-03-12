using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public async void doorRaised()
    {
        //get the current position 
        Vector3 currentPosition = transform.position;

        //later change this to an animation trigger
        currentPosition.y += 2f;
        transform.position = currentPosition;

    }
}
