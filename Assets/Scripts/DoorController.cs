using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, Interactable
{
    //public GameObject door;
    public string code;
    public GameObject childDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && InventoryController.S.hasKey)
        {
            StartCoroutine(Interact());
        }
    }

    public IEnumerator Interact()
    {
        OpenDoor(); 
        yield break; 
    }

    public void OpenDoor()
    {
        childDoor.GetComponent<BoxCollider2D>().enabled = false;
        childDoor.GetComponent<SpriteRenderer>().enabled = false;
    }
}
