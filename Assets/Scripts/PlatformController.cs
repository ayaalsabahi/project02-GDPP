using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public PlatformEffector2D effector;
    // public GameObject player;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(player.transform.position.y < gameObject.transform.position.y)
    //     {
    //         gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //     }
    //     else{
    //         gameObject.GetComponent<BoxCollider2D>().enabled = true;
    //     }
        
    // }
}
