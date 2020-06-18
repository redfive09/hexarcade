using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSelection : MonoBehaviour
{
private Dictionary<string, float> bestTimes;
    private bool switchToFirstPage = true;
    [SerializeField] private GameObject timeRecords;
    [SerializeField] private GameObject menuPage01;
    [SerializeField] private GameObject menuPage02;

    private void Start()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        ShowTimeRecords();
        SwitchPage();
    }

    // Shows time records below their corresponding level
    private void ShowTimeRecords()
    {
        TextMeshProUGUI[] records = timeRecords.GetComponentsInChildren<TextMeshProUGUI>();

        // for (int i = 0; i < records.Length; i++)
        // {
        //     float score = bestTimes[i+1];
        //     if (score > 0.0f)
        //     {
        //         records[i].text = Timer.GetTimeAsString(score);
        //     }
        //     else
        //     {
        //         records[i].text = "No Time Record";
        //     }
        // }
    }

    public void SwitchPage()
    {
        if(switchToFirstPage)
        {
            menuPage02.SetActive(false);
            menuPage01.SetActive(true);
        }
        else
        {
            menuPage01.SetActive(false);
            menuPage02.SetActive(true);
        }
        ShowTimeRecords();
        switchToFirstPage = !switchToFirstPage;
    }

    public void ResetRecords()
    {
        bestTimes = SaveLoadManager.ResetTimes();
        SaveLoadManager.SaveTimes(bestTimes);
        ShowTimeRecords();
    }

    //Show Level 1 from Scenes In Build
    public void Level01()
    {
        LoadLevel(1);
    }

    //Show Level 2 from Scenes In Build
    public void Level02()
    {
        LoadLevel(2);
    }

    //Show Level 3 from Scenes In Build
    public void Level03()
    {
        LoadLevel(3);
    }

    //Show Level 4 from Scenes In Build
    public void Level04()
    {
        LoadLevel(4);
    }

    //Show Level 5 from Scenes In Build
    public void Level05()
    {
        LoadLevel(5);
    }

    //Show Level 6 from Scenes In Build
    public void Level06()
    {
        LoadLevel(6);
    }

    //Show Level 7 from Scenes In Build
    public void Level07()
    {
        LoadLevel(7);
    }

    //Show Level 8 from Scenes In Build
    public void Level08()
    {
        LoadLevel(8);
    }

    //Show Level 9 from Scenes In Build
    public void Level09()
    {
        LoadLevel(9);
    }

    //Show Level 10 from Scenes In Build
    public void Level10()
    {
        LoadLevel(10);
    }

    //Show Level 11 from Scenes In Build
    public void Level11()
    {
        LoadLevel(11);
    }

        //Show Level 11 from Scenes In Build
    public void Level12()
    {
        LoadLevel(12);
    }

        //Show Level 11 from Scenes In Build
    public void Level13()
    {
        LoadLevel(13);
    }

        //Show Level 11 from Scenes In Build
    public void Level14()
    {
        LoadLevel(14);
    }

        //Show Level 11 from Scenes In Build
    public void Level15()
    {
        LoadLevel(15);
    }

        //Show Level 11 from Scenes In Build
    public void Level16()
    {
        LoadLevel(16);
    }

    // Determines which level scene is loaded
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}