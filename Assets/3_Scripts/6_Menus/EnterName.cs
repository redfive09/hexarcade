using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EnterName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private GameObject saveNameButton;
    private TouchScreenKeyboard touchScreenKeyboard;
    private TimeKeeper nameChanger;

    void Start()
    {        
        nameChanger = SaveLoadManager.Load();
        string playerName = nameChanger.GetPlayerName();
        nameDisplay.text = playerName;
        SceneTransitionValues.playerName = playerName.Trim();
        // EventSystem.currentSystem.SetSelectedGameObject(nameDisplay.gameObject, null);
        // nameDisplay.OnPointerClick (null);
    }
  
    public void OpenKeyboard()
    {        
        touchScreenKeyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default, false, false, false, false, "", 10);
    }

    public void SaveButtonAppear()
    {
        saveNameButton.SetActive(true);
    }

    public void SaveName()
    {
        nameChanger.SetPlayerName(nameDisplay.text);
        SaveLoadManager.Save(nameChanger);
        SceneTransitionValues.playerName = nameDisplay.text;
        saveNameButton.SetActive(false);
        Phone.Vibrate();
    }

    void Update()
    {
        if(TouchScreenKeyboard.visible == false && touchScreenKeyboard != null)
        {
            // Debug.Log(touchScreenKeyboard.status.GetType());
            if(touchScreenKeyboard.done);
            {
                nameDisplay.text = touchScreenKeyboard.text.Trim();
                touchScreenKeyboard = null;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) && saveNameButton.activeSelf)
        {
            SaveName();
        }
    }
}
