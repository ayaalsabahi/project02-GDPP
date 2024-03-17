using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textManager : MonoBehaviour
{
    public TMP_Text dialogueBox;
    //dialogue strings
    private string keyLocked = "You are too small to fit through!";
    private string doorLocked = "You need a key first...";

    private void Start()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void keyText()
    {
        dialogueBox.text = keyLocked;
        dialogueBox.gameObject.SetActive(true);
        Invoke("HideText", 3); //invoke the hide test funciton after 3 seconds 

    }


    public void doorText()
    {
        dialogueBox.text = doorLocked;
        dialogueBox.gameObject.SetActive(true);
        Invoke("HideText", 3); //invoke the hide test funciton after 3 seconds 
    }

    void HideText()
    {
        // Hide the text
        dialogueBox.gameObject.SetActive(false);
    }

}
