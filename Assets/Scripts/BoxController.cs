// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BoxController : MonoBehaviour, Interactable
// {
//     public BoxCollider2D col;
//     public Rigidbody2D rb;
//     public GameObject boy;
//     public GameObject top;
//     public Vector3 feetPos;
//     public bool boxOn;
//     // Start is called before the first frame update
//     void Start()
//     {
//         col = gameObject.GetComponent<BoxCollider2D>();
//         rb = gameObject.GetComponent<Rigidbody2D>();
//         feetPos = new Vector3(0, -0.5f, 0);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Debug.Log("boy.transform.position + feetPos"+ (boy.transform.position + feetPos));
//         Debug.Log("top.transform.position.y"+ top.transform.position.y);
//         if(!boxOn)
//         {
//             BoyCheck();
//         }
//     }
    
//     public IEnumerator Interact()
//     { 
//         Debug.Log("touching box")
// ;        if(col.enabled == true)
//         {
//             boxOn = false;
//             col.enabled = false;
//             top.GetComponent<BoxCollider2D>().enabled = false;
//             top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
//             rb.constraints = RigidbodyConstraints2D.FreezeAll;
//         }
//         else{
//             boxOn = true;
//             col.enabled = true;
//             top.GetComponent<BoxCollider2D>().enabled = true;
//             top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
//             rb.gravityScale = 1;
//             rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//             top.GetComponent<Rigidbody2D>().gravityScale = 1;
//         }
//         yield break;
//     }

//    public void BoyCheck()
//    {
//     if((boy.transform.position + feetPos).y < top.transform.position.y && !boxOn)
//     {
//         top.GetComponent<BoxCollider2D>().enabled = false;
//     }
//     else{
//         top.GetComponent<BoxCollider2D>().enabled = true;   
//     }
//    }
// }

//////
///

// using System.Collections;
// using UnityEngine;

// public class BoxController : MonoBehaviour, Interactable
// {
//     public BoxCollider2D col;
//     public Rigidbody2D rb;
//     public GameObject boy;
//     public GameObject top;
//     public Vector3 feetPos;
//     public bool boxOn;

//     void Start()
//     {
//         col = GetComponent<BoxCollider2D>();
//         rb = GetComponent<Rigidbody2D>();
//         feetPos = new Vector3(0, -0.5f, 0);
//     }

//     // void Update()
//     // {
//     //     if (!boxOn)
//     //     {
//     //         BoyCheck();
//     //     }
//     //     else
//     //     {
//     //         // When the box is on, make the top a child of the box to move with it
//     //         top.transform.SetParent(transform);
//     //         // Optionally, adjust the local position of the top to be on the box surface
//     //         top.transform.localPosition = new Vector3(0, col.size.y / 2 + top.GetComponent<BoxCollider2D>().size.y / 2, 0);
//     //     }
//     // }

//     // public IEnumerator Interact()
//     // { 
//     //     Debug.Log("Touching box");
//     //     if (col.enabled)
//     //     {
//     //         boxOn = false;
//     //         col.enabled = false;
//     //         // Disable top's collider and freeze its physics when the box is off
//     //         top.GetComponent<BoxCollider2D>().enabled = false;
//     //         top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
//     //         rb.constraints = RigidbodyConstraints2D.FreezeAll;
//     //     }
//     //     else
//     //     {
//     //         boxOn = true;
//     //         col.enabled = true;
//     //         // Re-enable top's collider but keep physics frozen
//     //         top.GetComponent<BoxCollider2D>().enabled = true;
//     //         top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
//     //         rb.gravityScale = 1;
//     //         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//     //         top.GetComponent<Rigidbody2D>().gravityScale = 0; // Prevent top from falling
//     //     }
//     //     yield break;
//     // }

//     void Update()
//     {
//         if(boxOn)
//         {
//             // Keep the top's rotation fixed
//             top.transform.rotation = Quaternion.identity; // This sets the rotation to "no rotation"
//         }

//         if (!boxOn)
//         {
//             BoyCheck();
//         }
//     }

//     // public IEnumerator Interact()
//     // {
//     //     Debug.Log("Touching box");
//     //     if (col.enabled == true)
//     //     {
//     //         boxOn = false;
//     //         col.enabled = false;
//     //         top.GetComponent<BoxCollider2D>().enabled = false;
//     //         top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
//     //         rb.constraints = RigidbodyConstraints2D.FreezeAll;
//     //     }
//     //     else
//     //     {
//     //         boxOn = true;
//     //         col.enabled = true;
//     //         top.GetComponent<BoxCollider2D>().enabled = true;
//     //         // Since the top is now moving with the box, freeze its rotation explicitly in Update()
//     //         // Note: We're not using FreezeRotation here because it's being managed in Update
//     //         rb.gravityScale = 1;
//     //         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//     //         top.GetComponent<Rigidbody2D>().gravityScale = 1;
//     //     }
//     //     yield break;
//     // }

//     public IEnumerator Interact()
//     {
//         Debug.Log("Touching box");
//         if (boxOn)
//         {
//             // If the box is currently on, turn it off
//             boxOn = false;
//             col.enabled = false;
//             rb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze box in place
//             top.GetComponent<BoxCollider2D>().enabled = false;
//             top.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
//         }
//         else
//         {
//             // If the box is currently off, turn it on
//             boxOn = true;
//             col.enabled = true;
//             rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Allow box to move and fall, but not rotate
//             rb.gravityScale = 1; // Ensure the box is affected by gravity and can fall
//             top.GetComponent<BoxCollider2D>().enabled = true;
//             // Don't need to adjust top's Rigidbody2D here if it's meant to stay with the box
//         }
//         yield break;
//     }

//     public void BoyCheck()
//     {
//         if ((boy.transform.position + feetPos).y < top.transform.position.y && !boxOn)
//         {
//             top.GetComponent<BoxCollider2D>().enabled = false;
//         }
//         else
//         {
//             top.GetComponent<BoxCollider2D>().enabled = true;
//         }
//     }
// }


//////2

using System.Collections;
using UnityEngine;
public class BoxController : MonoBehaviour, Interactable
{
    public BoxCollider2D col;  // Collider for the box
    public Rigidbody2D rb;  // Rigidbody for the box
    public GameObject boy;  // Reference to the boy character
    public GameObject top;  // Reference to the top of the box
    public Vector3 feetPos;  // Position of the boy's feet
    public bool boxOn;  // State of the box (tangible or intangible)
    public float yOffset;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        feetPos = new Vector3(0, -0.5f, 0);
        yOffset = 0.446858f;
        //-0.4468589 perfect
    }

    void Update()
    {
        if (!boxOn)
        {
            BoyCheck();  // Check if the boy is above the box to make the top tangible
        }
        else
        {
            // Ensure the top's collider is enabled when the box is tangible
            top.GetComponent<BoxCollider2D>().enabled = true;
            // Optionally, ensure the top is always in the correct position relative to the box
            // You might adjust this based on your game's physics and how you want the top to behave
            top.transform.localPosition = new Vector3(0, col.size.y / 2 + top.GetComponent<BoxCollider2D>().size.y / 2 - yOffset, 0);
        }
    }

    public IEnumerator Interact()
    {
        boxOn = !boxOn;  // Toggle the state of the box
        col.enabled = boxOn;  // Enable/disable the box's collider based on its state
        rb.constraints = boxOn ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezeAll;  // Allow movement if tangible
        rb.gravityScale = boxOn ? 1 : 0;  // Apply gravity only when the box is tangible

        // The top's collider should be managed in the Update or BoyCheck methods depending on the box's state
        // so we don't need to change it here

        yield break;
    }

    public void BoyCheck()
    {
        // Make the top tangible only if the boy is above the top and the box is intangible
        top.GetComponent<BoxCollider2D>().enabled = (boy.transform.position + feetPos).y >= top.transform.position.y && !boxOn;
    }
}

//works but stops