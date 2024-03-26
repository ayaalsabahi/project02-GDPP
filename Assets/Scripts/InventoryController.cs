using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController S;

    //public Transform targetObject; // The object whose position the sprite will follow
    //public bool followMainCamera = true; // Flag to determine whether to follow the main camera or secondary camera

    //public GameObject possessCamera;
    //public GameObject dollCamera;

    ArrayList playerInventory = new ArrayList();
    public bool hasKey1;
    public bool hasKey2;
    public bool hasKey3;

    // Start is called before the first frame update
    void Awake()
    {
        S = this; // singleton definition
    }

    public void AddObject(string obj)
    {
        playerInventory.Add(obj);
        if(obj == "Key1")
        { 
            hasKey1 = true;
            Debug.Log(hasKey1);
            //this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (obj == "Key2")
        {
            hasKey2 = true;
            Debug.Log(hasKey2);
            //this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (obj == "Key3")
        {
            hasKey3 = true;
            Debug.Log(hasKey3);
            //this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


}
