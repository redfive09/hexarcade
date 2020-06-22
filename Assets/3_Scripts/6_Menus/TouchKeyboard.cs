using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEngine.UI;

public class TouchKeyboard : MonoBehaviour
{
    private string textInput = ""; //Indtast adresse
    [SerializeField] private GameObject textDisplay;
      
     private TouchScreenKeyboard keyboard;
 
     void OnMouseUpAsButton() {
     
         TouchScreenKeyboard.hideInput = true;
         if(textInput == "No input") {
             textInput = "";
         }
         keyboard = TouchScreenKeyboard.Open(textInput, TouchScreenKeyboardType.ASCIICapable,false,false,false,false);
 
     }
 
 
     // Update is called once per frame
     void Update () {
     
         if(keyboard != null) {
 
             textInput = keyboard.text;
             textDisplay.GetComponent<TextMeshProUGUI>().text = textInput;
 
         }
 
         if(keyboard != null && keyboard.done == true) {
 
             print("done");
             keyboard = null;
             
         }
 
     }
}
