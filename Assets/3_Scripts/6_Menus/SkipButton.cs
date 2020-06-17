using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SkipButton : MonoBehaviour
{
    private bool buttonPressed = false;
    [SerializeField] TextMeshProUGUI timerField;


    /*
     *  This method is just here to be compatible with older maps, so the skip button doesn't have to be added again
     */
    public void SkipTutorial()
    {
        Skip();
    }

    public void Skip()
    {
        buttonPressed = true;
    }

    public bool IsButtonPressed()
    {
        return buttonPressed;
    }

    public void ResetForCheckpoints()
    {
        timerField.text = "Confirm";
        buttonPressed = false;
    }

    public void Reset()
    {
        timerField.text = "Skip";
        buttonPressed = false;
    }

    public bool HasSkipButtonReference()
    {
        return timerField != null;
    }
}
