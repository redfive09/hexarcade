using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSelection : MonoBehaviour
{
    // [SerializeField] private GameObject timeRecords;    
    // [SerializeField] private GameObject menuPage01;
    // [SerializeField] private GameObject menuPage02;
    // [SerializeField] private GameObject LevelAtPos1;


    private Dictionary<string, float> bestTimes;
    private Dictionary<string, List<string>> worlds = new Dictionary<string, List<string>>();
    private SortedSet<string> worldList = new SortedSet<string>();
    
    
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
        ManageLevels();
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


    private void ManageLevels()
    {        
        int skipNamePart = ".unity".Length;                                             // every scene ends with that extension, so we can remember it (respectively its length) in order to save some time
                
        for(int i = 3; i < SceneManager.sceneCountInBuildSettings - 1; i++)             // first level starts at build 3 and last scene is not a level either
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);                // thx to this -> https://stackoverflow.com/questions/40898310/get-name-of-next-scene-in-build-settings-in-unity3d/40901893
            string worldName = "";
            string levelName = "";
            int firstSlashAtIndex = 0;
            int indexEndOfLevelName = scenePath.Length - skipNamePart - 1;              // we need -1, because the index starts at 0
            
            for(int j = indexEndOfLevelName; j >= 0; j--)
            {                
                string namePart = scenePath.Substring(j, 1);                            // get a single char, which we need for checking, if it's a slash character
                if(namePart == "/" && firstSlashAtIndex == 0)                           // if it hits the first slash, then we found the beginning of the level name
                {
                    firstSlashAtIndex = j;                                              // remember the position, we need it to differentiate between the world and level names
                    levelName = scenePath.Substring(j + 1, indexEndOfLevelName - j);    // we don't need the slash itself, that's why we need +1; the substraction returns us the amount of steps we gone so far
                }
                else if (namePart == "/" && firstSlashAtIndex != 0)                     // if it wasn't the first slash, then we found the beginning of the world name
                {
                    worldName = scenePath.Substring(j + 1, firstSlashAtIndex - j - 1);  // -1 since we don't want the slash-char to be part of the name
                    
                    if(worlds.TryGetValue(worldName, out List<string> levels))          // check, if we saved the world already
                    {
                        levels.Add(levelName);                                          // add the level to the existing world
                    }
                    else                                                                // in case the world does not exist, yet:
                    {
                        List<string> levelList = new List<string>();                    // create a new list for its levels
                        levelList.Add(levelName);                                       // add the new level
                        worlds[worldName] = levelList;                                  // give the level list to the world
                        worldList.Add(worldName);                                      // and save the world name as well
                    }
                    break;                                                              // we found everything we need, so let's go to the next level
                }
            }
        }
    }

    private void PrintAllLevels()
    {
        foreach (string world in worldList)
        {
            List<string> levelList = worlds[world];

            foreach (string level in levelList)
            {
                Debug.Log(level + " in " + world);
            }
        }
    }



    // Shows time records below their corresponding level
    private void ShowTimeRecords()
    {
        // TextMeshProUGUI[] records = timeRecords.GetComponentsInChildren<TextMeshProUGUI>();

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




    public void ResetRecords()
    {
        bestTimes = SaveLoadManager.ResetTimes();
        SaveLoadManager.SaveTimes(bestTimes);
        ShowTimeRecords();
    }

    //Show Level 1 from Scenes In Build
    public void Level1()
    {
        // LevelAtPos1.transform.position = new Vector3(LevelAtPos1.transform.position.x - 100, 0, 0);
    }

    //Show Level 2 from Scenes In Build
    public void Level2()
    {
        LoadLevel(2);
    }

    //Show Level 3 from Scenes In Build
    public void Level3()
    {
        LoadLevel(3);
    }

    // Determines which level scene is loaded
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}