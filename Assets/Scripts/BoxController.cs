using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, Interactable
{
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Interact()
    { 
        Debug.Log("tuuu");
        yield break;
    }
}
