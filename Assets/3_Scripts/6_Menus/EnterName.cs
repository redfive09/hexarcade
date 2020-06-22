using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject saveNameButton;
    private TouchScreenKeyboard touchScreenKeyboard;

  
    public void OpenKeyboard()
    {
        touchScreenKeyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default, false, false, false, false, "", 10);              
        Debug.Log("trigger");
    }

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    public void SaveButtonAppear()
    {
        saveNameButton.SetActive(true);
    }

    public void SaveName()
    {
        saveNameButton.SetActive(false);
    }

    void Update()
    {
        if(TouchScreenKeyboard.visible == false && touchScreenKeyboard != null)
        {
            // Debug.Log(touchScreenKeyboard.status.GetType());
            if(touchScreenKeyboard.done);
            {
                textDisplay.text = touchScreenKeyboard.text;
                touchScreenKeyboard = null;                                
            }
        }
    }
}
