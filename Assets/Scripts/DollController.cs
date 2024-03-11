using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DollController : MonoBehaviour
{
    public DollProperties properties;
    public float speed;
    public float jump;
    public float gravity;
    public Sprite sprite;

    void Start()
    {
        speed = properties.moveSpeed;
        jump = properties.jumpStrength;
        gravity = properties.gravity;
        sprite = properties.dollSprite;

        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void OnPossessed()
    {
        // Logic for when the doll is possessed
    }

    public void OnReleased()
    {
        // Logic for when the doll is released
    }
}