using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class WinScreen : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI newRecord;
    [SerializeField] private GameObject highscores;

    void Start()
    {
        SceneTransitionValues.lastMenuName = SceneManager.GetActiveScene().name;
        OneTimeWinningScreenEvents();

        if(SceneTransitionValues.newRecord)
        {
            newRecord.enabled = true;
            newRecord.gameObject.SetActive(true);
            newRecord.text += Timer.GetTimeAsString(SceneTransitionValues.time, 3);          
        }
        else
        {
            time.enabled = true;
            time.gameObject.SetActive(true);
            time.text += Timer.GetTimeAsString(SceneTransitionValues.time, 3);

            this.record.enabled = true;
            this.record.gameObject.SetActive(true);
            this.record.text += Timer.GetTimeAsString(SceneTransitionValues.record, 3);
        }
    }

    private void OneTimeWinningScreenEvents()
    {
        if(!SceneTransitionValues.alreadyEnteredWinningScreen)
        {
            Phone.Vibrate();
            if(SceneTransitionValues.newRecord)
            {            
                UploadTime();            
            }
        }        
        SceneTransitionValues.alreadyEnteredWinningScreen = true;
    }

    private void UploadTime()
    {
        highscores = Instantiate(highscores);

        string playerName = SceneTransitionValues.playerName;
        if(String.IsNullOrEmpty(playerName))
        {
            playerName = SaveLoadManager.Load().GetPlayerName();
        }
        Highscores.AddNewHighscore(SceneTransitionValues.currentSceneName, playerName, SceneTransitionValues.time);
    }
}