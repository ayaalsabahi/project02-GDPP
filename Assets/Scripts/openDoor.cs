using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public void doorRaised()
    {
        //get the current position 
        Vector3 currentPosition = transform.position;

        //add 30 to it
        currentPosition.y += 30f;

        // Update the object's position
        transform.position = currentPosition;



    }
}
