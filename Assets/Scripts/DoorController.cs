using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //public GameObject door;
    public string code;
    public GameObject childDoor;
    public string keyToUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keyToUnlock == "Key1")
        {
            if (collision.gameObject.tag == "Player" && InventoryController.S.hasKey1)
            {
                StartCoroutine(Interact(childDoor));
            }
        }
        else if (keyToUnlock == "Key2")
        {
            if (collision.gameObject.tag == "Player" && InventoryController.S.hasKey2)
            {
                StartCoroutine(Interact(childDoor));
            }
        }
        else if (keyToUnlock == "Key3")
        {
            if (collision.gameObject.tag == "Player" && InventoryController.S.hasKey3)
            {
                StartCoroutine(Interact(childDoor));
            }
        }
    }

    public IEnumerator Interact(GameObject childDoor)
    {
        OpenDoor(childDoor); 
        yield break; 
    }

    public void OpenDoor(GameObject childDoor)
    {
        childDoor.GetComponent<BoxCollider2D>().enabled = false;
        childDoor.GetComponent<SpriteRenderer>().enabled = false;
    }
}
