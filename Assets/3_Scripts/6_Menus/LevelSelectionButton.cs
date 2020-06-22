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
        levelName = name;
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

    public void SetInactive()
    {
        transform.gameObject.SetActive(false);
    }

    public void LevelSelected()
    {
        SceneManager.LoadScene(levelName);
    }
}