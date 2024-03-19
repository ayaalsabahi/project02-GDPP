using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Key1")
            {
                InventoryController.S.AddObject(this.gameObject.tag);
                // increase player speed
                // play key get sound
                Destroy(this.gameObject);
            }
            else
            {
            }

        }
    }
}
