using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour, Interactable
{
    public GameObject lever;
    public Rigidbody2D leverRB;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lever = gameObject.transform.GetChild(0).gameObject;
        leverRB = lever.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box")) // Ensure your box GameObject is tagged "Box" in the editor
        {
            StartCoroutine(Interact());
        }
    }

    public IEnumerator Interact()
    { 
        // Unfreeze y-axis movement
        leverRB.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        // Move up for 1 second
        float endTime = Time.time + 5f; // Set end time for 1 second from now
        while (Time.time < endTime)
        {
            leverRB.velocity = new Vector2(0, moveSpeed/5); // Move up at the specified speed
            yield return null; // Wait for the next frame
        }

        // Stop the movement
        leverRB.velocity = Vector2.zero;

        // Refreeze y-axis movement
        leverRB.constraints |= RigidbodyConstraints2D.FreezePositionY;
    }
}
