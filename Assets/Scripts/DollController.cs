using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DollController : MonoBehaviour
{
    public DollProperties properties;
    public string DollName;
    public float speed;
    public float jump;
    public float gravity;
    public Sprite sprite;

    void Start()
    {
        DollName = properties.dollName;
        speed = properties.moveSpeed;
        jump = properties.jumpStrength;
        gravity = properties.gravity;
        sprite = properties.dollSprite;

        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        Debug.Log("doll loctaion" + transform.position);
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
        Show();
    }

    public string GetName()
    {
        return DollName;
    }

    public void SendToGhost(Vector2 location)
    {
        transform.position = location;
    }
}