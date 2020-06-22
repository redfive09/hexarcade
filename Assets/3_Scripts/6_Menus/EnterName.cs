using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterName : MonoBehaviour
{
    [SerializeField] private string theName;
    [SerializeField] private GameObject inputField;
    [SerializeField] private GameObject textDisplay;

    private TouchScreenKeyboard touchScreenKeyboard;    

    private string textInput = ""; //Indtast adresse
      
     private TouchScreenKeyboard keyboard;
 
    //  void OnMouseUpAsButton() {
     
    //      TouchScreenKeyboard.hideInput = true;
    //      if(textInput == "No input") {
    //          textInput = "";
    //      }
    //      keyboard = TouchScreenKeyboard.Open(textInput, TouchScreenKeyboardType.ASCIICapable,false,false,false,false);
 
    //  }
 
 
    //  // Update is called once per frame
    //  void Update () {
     
    //      if(keyboard != null) {
 
    //          textInput = keyboard.text;
    //          textDisplay.GetComponent<TextMeshProUGUI>().text = textInput;
 
    //      }
 
    //      if(keyboard != null && keyboard.done == true) {
 
    //          print("done");
    //          keyboard = null;
             
    //      }
 
    //  }









    public void OpenKeyboard()
    {
        touchScreenKeyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default);
        // touchScreenKeyboard = TouchScreenKeyboard.Open(textInput, TouchScreenKeyboardType.ASCIICapable,false,false,false,false);
        Debug.Log("triggered");
    }

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    void Update()
    {
        if(TouchScreenKeyboard.visible == false && touchScreenKeyboard != null)
        {
            // Debug.Log(touchScreenKeyboard.status.GetType());
            if(touchScreenKeyboard.done);
            {
                textInput = touchScreenKeyboard.text;
                textDisplay.GetComponent<TextMeshProUGUI>().text = "Hi " + textInput;
                touchScreenKeyboard = null;
            }
        }
    }


    // public void StoreName()
    // {
    //     theName = touchScreenKeyboard.text;
    //     textDisplay.GetComponent<TextMeshProUGUI>().text = "Welcome " + theName + " to the game";
    // }

}
