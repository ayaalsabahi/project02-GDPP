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

    public void Awake()
    {
        player1Controller = player1.GetComponent<PlayerController>();
        player2Controller = player2.GetComponent<PlayerController>();
    }


    public void SwitchPlayer()
    {
        if(player1Active)
        {
            player1Controller.enabled = false;
            player2Controller.enabled = true;
            player1Active = false;
        }
        else{
            player2Controller.enabled = false;
            player1Controller.enabled = true;
            player1Active = true;
        }
    }

}
