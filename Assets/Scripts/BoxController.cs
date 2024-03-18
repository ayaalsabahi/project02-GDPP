using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, Interactable
{
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Interact()
    { 
        if(collider.enabled == true)
        {
            collider.enabled = false;
            rb.gravityScale = 0;
        }
        else{
            collider.enabled = true;
            rb.gravityScale = 1;
        }
        yield break;
    }
}