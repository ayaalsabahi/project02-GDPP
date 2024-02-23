using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollisions : MonoBehaviour
{

    [Header("Events")]

    public GameEvent gotKey;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key1"))
        {
            gotKey.Raise();
        }
    }

}
