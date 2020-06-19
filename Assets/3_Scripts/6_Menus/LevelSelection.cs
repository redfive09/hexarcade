using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject levelFolder;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject previousWorld;
    [SerializeField] private GameObject nextWorld;
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private TMP_Text currentWorld;
    
    

    private const int FIRST_LEVEL_AT_BUILD_INDEX = 3;
    private int currentPage = 0;
    private int maxPages;
    private Dictionary<string, float> bestTimes;
    private Dictionary<string, List<string>> worlds = new Dictionary<string, List<string>>();
    private List<string> worldList = new List<string>();
    
    
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
        
        ManageLevels();
        SetWorldSelection();
        OrganiseLevelPages();
        // PrintAllLevels();
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
                
        for(int i = FIRST_LEVEL_AT_BUILD_INDEX; 
                i < SceneManager.sceneCountInBuildSettings - 1; i++)                    // iterate only over all levels (last scene is this level selection screen)
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
        currentWorld.text = worldList[0];
    }

    private void SetWorldSelection()
    {
        currentWorld.text = worldList[0];
    }

    private void ShowLevels()
    {
        List<string> levels = worlds[currentWorld.text];
        int maxLevelsPerPage = levelFolder.transform.childCount;
        
        LevelSelectionButton[] levelButtons = levelFolder.GetComponentsInChildren<LevelSelectionButton>();
        
        for(int i = 0; i < maxLevelsPerPage; i++)
        {
            int currentLevel = currentPage * maxLevelsPerPage + i;
            if(currentLevel < levels.Count)
            {
                string levelName = levels[currentLevel];
                
                string record = "";
                if(bestTimes.TryGetValue(levelName, out float playersRecord)) // mayber level was never played or finished
                {
                    record = Timer.GetTimeAsString(playersRecord, 3);
                }

                Sprite levelImage = Resources.Load<Sprite>("8_LevelImages/" + currentWorld.text + "/" + levelName);
                if(levelImage == null)
                {
                    levelImage = Resources.Load<Sprite>("8_LevelImages/defaultImage");
                }
                levelButtons[i].SetLevelData(levelName, record, levelImage);
            }
            else
            {
                levelButtons[i].SetInactive();
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

    private void CalcPages()
    {
        // maxPages   
    }


    public void ResetRecords()
    {
        bestTimes = SaveLoadManager.ResetTimes();
        SaveLoadManager.SaveTimes(bestTimes);
        ShowTimeRecords();
    }


    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void PreviousWorld()
    {       
        for(int i = 0; i < worldList.Count; i++)
        {            
            if(worldList[i] == currentWorld.text)
            {                
                int setNewWorld = i - 1;
                if(setNewWorld < 0) setNewWorld = worldList.Count - 1;
                currentWorld.text = worldList[setNewWorld];
                break;
            }
        }
        OrganiseLevelPages();
    }

    public void NextWorld()
    {
        for(int i = 0; i < worldList.Count; i++)
        {
            if(worldList[i] == currentWorld.text)
            {
                int setNewWorld = (i + 1) % worldList.Count;
                currentWorld.text = worldList[setNewWorld];
                break;
            }
        }
        OrganiseLevelPages();
    }

    private void OrganiseLevelPages()
    {
        currentPage = 0;
        ShowLevels();
        
    }

    public void PreviousLevelPage()
    {
        currentPage--;              // boundary check!!!
        OrganiseLevelPages();
    }

    public void NextLevelPage()
    {
        currentPage++;              // boundary check!!!
        OrganiseLevelPages();
    }
}