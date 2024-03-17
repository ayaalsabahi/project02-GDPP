using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, Interactable
{
    public GameObject door;
    public string code;
    public IEnumerator Interact()
    {
        OpenDoor(); 
        yield break; 
    }

    public void OpenDoor()
    {
        door.GetComponent<SpriteRenderer>().enabled = false;
        door.GetComponent<BoxCollider2D>().enabled = false;
    }
}
