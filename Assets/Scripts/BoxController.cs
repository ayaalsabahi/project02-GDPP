using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, Interactable
{
    public BoxCollider2D col;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator Interact()
    { 
        Debug.Log("touching box")
;        if(col.enabled == true)
        {
            col.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else{
            col.enabled = true;
            rb.gravityScale = 1;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        yield break;
    }
}
