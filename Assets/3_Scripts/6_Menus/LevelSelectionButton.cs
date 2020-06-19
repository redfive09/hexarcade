using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionButton : MonoBehaviour
{
    private string levelName;    

    public void SetLevelData(string name, string record, Sprite levelImage)
    {
        transform.gameObject.SetActive(true);
        
        levelName = name;
        TextMeshProUGUI[] textFields = GetComponentsInChildren<TextMeshProUGUI>();
        
        for(int i = 0; i < textFields.Length; i++)
        {            
            if(textFields[i].name == "Name")
            {
                textFields[i].text = name;
            }

            if(textFields[i].name == "Record")
            {
                textFields[i].text = record;
            }
        }
        GetComponentInChildren<Image>().sprite = levelImage;
    }

    public void SetInactive()
    {
        transform.gameObject.SetActive(false);
    }

    public void LevelSelected()
    {
        SceneManager.LoadScene(levelName);
    }
}