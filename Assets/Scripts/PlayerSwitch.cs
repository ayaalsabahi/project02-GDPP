using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject player1;
    [SerializeField]
    public GameObject player2;
    public PlayerController player1Controller;
    public PlayerController player2Controller;
    public bool player1Active = true;
    private Animator p1anim;

    public void Awake()
    {
        player1Controller = player1.GetComponent<PlayerController>();
        player2Controller = player2.GetComponent<PlayerController>();
        p1anim = player1Controller.GetComponentInChildren<Animator>();
    }

    public void SwitchPlayer(Animator animator)
    {
        //Debug.Log("switchplayer called");
        if (player1Active)
        {
            player2Controller.enabled = true;
            if (p1anim) { p1anim.SetTrigger("isMouse"); }
            player1Controller.enabled = false;
            player1Active = false;
        }
        else {
            player1Controller.enabled = true;
            if (p1anim) { p1anim.SetTrigger("isBoy"); }
            player2Controller.enabled = false;
            //Debug.Log("got to here");
            player1Active = true;
            //Debug.Log("isBoy should be triggered");
        }
    }

}
