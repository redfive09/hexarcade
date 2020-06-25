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
            

    private const int FIRST_LEVEL_AT_BUILD_INDEX = 4;
    private int currentPage = 1;
    private int maxPages;
    private int maxLevelsPerPage;
    
    private Dictionary<string, List<string>> worlds = new Dictionary<string, List<string>>();
    private List<string> worldList = new List<string>();    
    
    
    private void Start()
    {
        if(!SceneTransitionAudio.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            SceneTransitionAudio.Instance.gameObject.GetComponent<AudioSource>().Play();
        }
        
        SceneTransitionValues.lastMenuName = SceneManager.GetActiveScene().name;
        maxLevelsPerPage = levelFolder.transform.childCount;
        
        ManageLevels();
        ShowLevels();
        // PrintAllLevels();
    }


    private void ManageLevels()
    {
        string ignore = "_Menus";                                                       // ignore scenes from a folder, which are not levels
        int skipNamePart = ".unity".Length;                                             // every scene ends with that extension, so we can remember it (respectively its length) in order to save some time
        List<string> allLevels = new List<string>();                                    // list for all levels
                
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
                    
                    if(worldName != ignore)
                    {
                        if(worlds.TryGetValue(worldName, out List<string> levels))      // check, if we saved the world already
                        {
                            levels.Add(levelName);                                      // add the level to the existing world
                            allLevels.Add(levelName);                                   // add the level to a list with all levels
                        }
                        else                                                            // in case the world does not exist, yet:
                        {
                            List<string> levelList = new List<string>();                // create a new list for its levels
                            levelList.Add(levelName);                                   // add the new level
                            allLevels.Add(levelName);                                   // add the level to a list with all levels
                            worlds[worldName] = levelList;                              // give the level list to the world
                            worldList.Add(worldName);                                   // and save the world name as well
                        }
                    }                    
                    break;                                                              // we found everything we need, so let's go to the next level
                }
            }
        }        
        SceneTransitionValues.allLevels = allLevels;
        currentWorld.text = worldList[0];
        CalculatePages();
    }

    private void ShowLevels()
    {
        for(int i = 0; i < maxLevelsPerPage; i++)        
        {
            levelFolder.transform.GetChild(i).gameObject.SetActive(true);                                   // make sure all buttons are activates, otherwise we cannot acces their scripts
        }

        List<string> levels = worlds[currentWorld.text];                                                    // get the levels of the current world        
        LevelSelectionButton[] levelButtons = levelFolder.GetComponentsInChildren<LevelSelectionButton>();  // save all button scripts
                                                                                                            // prepare variable to make sure, if the next level can be played already
        
        for(int i = 0; i < maxLevelsPerPage; i++)                                                           // go through all buttons of the page
        {
            int currentLevel = (currentPage - 1) * maxLevelsPerPage + i;                                    // calculate the current level
            if(currentLevel < levels.Count)                                                                 // make sure, if there is even a level left for the current button
            {
                string levelName = levels[currentLevel];                                                    // get the level name                
                levelButtons[i].SetLevelData(levelName);                                                    // give all the collected data to the button script
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);                                                // if there was no level left for this button, make it disappear
            }
        }        
    }

    private void WorldChanged(bool nextWorld)
    {
        for(int i = 0; i < worldList.Count; i++)
        {
            if(worldList[i] == currentWorld.text)
            {
                if(nextWorld)
                {
                    int setNewWorld = (i + 1) % worldList.Count;
                    currentWorld.text = worldList[setNewWorld];
                }
                else
                {
                    int setNewWorld = i - 1;
                    if(setNewWorld < 0) setNewWorld = worldList.Count - 1;
                    currentWorld.text = worldList[setNewWorld];
                }
                break;
            }            
        }
        currentPage = 1;
        CalculatePages();        
        ShowLevels();
    }

    private void CalculatePages()    
    {
        maxPages = Mathf.CeilToInt((float) worlds[currentWorld.text].Count / maxLevelsPerPage);     // amount of levels divided by number of buttons and then round up
    }

    public void PreviousLevelPage()
    {
        if(currentPage - 1 > 0)
        {           
            currentPage--;            
        }
        else
        {
            currentPage = maxPages;            
        }
        ShowLevels();
    }

    public void NextLevelPage()
    {
        if(currentPage + 1 <= maxPages)
        {
            currentPage++;                        
        }
        else
        {
            currentPage = 1;
        }
        ShowLevels();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void PreviousWorld()
    {       
        WorldChanged(false);
    }

    public void NextWorld()
    {
        WorldChanged(true);
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

    

}

// public class WorldAndLevels
// {
//     private Dictionary<string, List<string>> worlds;
//     private List<string> worldList;
//     private List<string> allLevels;

//     public WorldAndLevels(Dictionary<string, List<string>> worlds, List<string> worldList, List<string> allLevels)
//     {
//         this.worlds = worlds;
//         this.worldList = worldList;
//         this.allLevels = allLevels;
//     }

//     public Dictionary<string, List<string>> GetWorlds()
//     {
//         return worlds;
//     }

//     public List<string> GetWorldList()
//     {
//         return worldList;
//     }

//     public List<string> GetLevelList()
//     {
//         return allLevels;
//     }
// }