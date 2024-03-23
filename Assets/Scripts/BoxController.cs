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
    public GameObject mouse;
    public GameObject playerSwitcher;

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
            PlayerCheck();
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
    public void PlayerCheck()
    {
        if(playerSwitcher.GetComponent<PlayerSwitch>().player1Active)
        {
            BoyCheck();
        }
        else{
            MouseCheck();
        }
    }

    public void BoyCheck()
    {
        // Make the top tangible only if the boy is above the top and the box is intangible
        top.GetComponent<BoxCollider2D>().enabled = (boy.transform.position + feetPos).y >= top.transform.position.y && !boxOn;
    }

    public void MouseCheck()
    {
        // Make the top tangible only if the boy is above the top and the box is intangible
        top.GetComponent<BoxCollider2D>().enabled = (mouse.transform.position + feetPos).y >= top.transform.position.y && !boxOn;
    }
}

//works but stops