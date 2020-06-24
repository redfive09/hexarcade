using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionButton : MonoBehaviour
{
    private string levelName;    

    public void SetLevelData(string name)
    {     
        levelName = name;
        string record = GetTime();
        Sprite levelImage = GetSprite();
        
        TextMeshProUGUI[] textFields = GetComponentsInChildren<TextMeshProUGUI>();

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "Name")
            {
                textFields[i].text = name;
            }
            
            if(transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).name == "Record")
            {
                textFields[i].text = record;
            }

            if(transform.GetChild(i).name == "Image")
            {
                transform.GetChild(i).GetComponent<Image>().sprite = levelImage;
            }
        }
    }

    private string GetTime()
    {
        Dictionary<string, float> bestTimes = SaveLoadManager.LoadTimes();
        string record = "";

        if(bestTimes.TryGetValue(levelName, out float playersRecord))                               // prevent an error in case the level was never finished before
        {
            record = Timer.GetTimeAsString(playersRecord, 3);                                       // if if was finished before, save the record as a string
        }
        return record;
    }

    private Sprite GetSprite()
    {
        Sprite levelImage = Resources.Load<Sprite>(levelName);                                      // get the level picture                
               
        if(levelImage == null)                                                                      // in case it didn't find a picture
        {
            levelImage = Resources.Load<Sprite>("defaultImage");                                    // get a default picture
        }
        return levelImage;
    }

    public void SetInactive()
    {
        transform.gameObject.SetActive(false);
    }

    public void LevelSelected()
    {
        SceneManager.LoadScene(levelName);
    }

    public void SeeHighscores()
    {
        SceneTransitionValues.currentSceneName = levelName;
        SceneManager.LoadScene("1_Scenes/_Menus/Highscores");
    }
}