using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class textPinManager : MonoBehaviour
{

    public TMP_Text headertext;
    public TMP_InputField inputField;

    private string insertPin = "Insert pin below:";
    private string isWrong = "The pin is wrong!";
    private string doorOpened = "Door unlocked!";
    private string letterStr = "Numbers only..."; 
    private string startType = "Type 4 numbers here";
    private string correctSequenceStr = "1234";

    bool isLetter = false;

    [Header("Events")]
    public GameEvent correctSequenceEvent;


    private void Start()
    {
        headertext.text = insertPin;
        
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = startType;
        inputField.onEndEdit.AddListener(checkNumber);
    }


    public void checkNumber(string inputStr)
    {
        if (UnityEngine.Input.GetKey(KeyCode.Return) || UnityEngine.Input.GetKey(KeyCode.KeypadEnter))
        {
            Debug.Log("reached checking");
            Debug.Log(inputStr);
            //if the number contains a letter
            foreach (char c in inputStr)
            {
                Debug.Log(c);
                if (char.IsLetter(c))
                {
                    headertext.text = letterStr;
                    isLetter = true;
                }
            }

            if (inputStr == correctSequenceStr)
            {
                correctSequenceEvent.Raise();
                headertext.text = doorOpened;
            }
            else if (!isLetter) headertext.text = isWrong;
            inputField.text = "";

        }
          

    }


}
