using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject timeRecords;    
    [SerializeField] private GameObject menuPage01;
    [SerializeField] private GameObject menuPage02;
    [SerializeField] private GameObject LevelAtPos1;


    private Dictionary<string, float> bestTimes;
    private bool switchToFirstPage = true;
    

    // Settings for control
    private const float MIN_DISTANCE_FOR_PANNING_RECOGNITION = 0.05f;
    private Camera cam;
    private Touch touch;
    private Vector3 touchStart;
    private bool touchPhaseEnded = true;
    private bool startedPanning = false;

    
    private void Start()
    {
        cam = Camera.main;
        bestTimes = SaveLoadManager.LoadTimes();
        // ShowTimeRecords();
        // SwitchPage();
    }

    private void Update()
    {
        if(Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            touchPhaseEnded = false;
        }

        if(Input.GetMouseButton(0)) // Dragging
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            direction = new Vector3(direction.x, 0, 0);
            if(direction.sqrMagnitude > MIN_DISTANCE_FOR_PANNING_RECOGNITION)
            {                
                cam.transform.position += direction;
                startedPanning = true;
            }
        }

        if(Input.GetMouseButtonUp(0) && !touchPhaseEnded) // Selecting
        {
            if(startedPanning)
            {
                startedPanning = false;
            }
            else
            {
                // Check if a level got choosen
            }
            touchPhaseEnded = true;   
        }
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
        LevelAtPos1.transform.position = new Vector3(LevelAtPos1.transform.position.x - 100, 0, 0);
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

    // Determines which level scene is loaded
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}