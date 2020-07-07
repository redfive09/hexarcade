using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    private List<Platform> platforms = new List<Platform>(); // Holds all platforms of the current level
    // private List<Dictionary<int, List<Hexagon>>> tileLists = new List<Dictionary<int, List<Hexagon>>>();

    private Dictionary<int, List<Hexagon>> crackedTiles = new Dictionary<int, List<Hexagon>>();         // 0
    private Dictionary<int, List<Hexagon>> pathTiles = new Dictionary<int, List<Hexagon>>();            // 1
    private Dictionary<int, List<Hexagon>> distractionTiles = new Dictionary<int, List<Hexagon>>();     // 2
    private Dictionary<int, List<Hexagon>> checkpointTiles = new Dictionary<int, List<Hexagon>>();      // 3
    private Dictionary<int, List<Hexagon>> specialTiles = new Dictionary<int, List<Hexagon>>();         // 4
    private Dictionary<int, List<Hexagon>> movingTiles = new Dictionary<int, List<Hexagon>>();          // 5
    private Dictionary<int, List<Hexagon>> startingTiles = new Dictionary<int, List<Hexagon>>();        // 6
    private Dictionary<int, List<Hexagon>> winningTiles = new Dictionary<int, List<Hexagon>>();         // 7
    private Dictionary<int, List<Hexagon>>[] tileLists;         // all Dictionaries will be added into this area
    private Dictionary<Hexagon, Color> tileColors = new Dictionary<Hexagon, Color>();
    

    // all tiles in beginning get added to this array, but be careful using it, since deletion of a Hexagon can occur during game and array does not get updated about that
    private Hexagon[] allTiles;

    private List<Hexagon> standardTiles = new List<Hexagon>();  // list of all tiles without any special purpose

    [SerializeField]
    private Material crackedHexagons;
    [SerializeField]
    private Material pathHexagons;
    [SerializeField]
    private Material distractionHexagons;
    [SerializeField]
    private Material checkpointHexagons;
    [SerializeField]
    private Material teleporterHexagons;
    [SerializeField]
    private Material velocityHexagons;
    [SerializeField]
    private Material jumpPadHexagons;
    [SerializeField]
    private Material loosingHexagons;
    [SerializeField]
    private Material movingHexagons;
    [SerializeField]
    private Material startingHexagons;
    [SerializeField]
    private Material winningHexagons;
    [SerializeField]
    private Material standardHexagons;

    /* ------------------------------ STARTING METHODS BEGINN ------------------------------  */
    public void GetStarted(bool inEditor)
    {        
        CollectTiles(inEditor);
        GetComponent<TileColorsIntroduction>().GetStarted(tileColors);        
    }

    public void CollectTiles(bool inEditor)
    {
        PrepareLists();
        ClearEverything();
        CollectPlatforms();
        CollectTilesForListsAndColorThem(inEditor);
    }

    private void PrepareLists()
    {
    tileLists = new Dictionary<int, List<Hexagon>>[] {
        crackedTiles,
        pathTiles,
        distractionTiles,
        checkpointTiles,
        specialTiles,
        movingTiles,
        startingTiles,
        winningTiles
        };
    }

    private void CollectPlatforms()
    {
        int numberOfTiles = 0;        
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Platform platform = this.transform.GetChild(i).GetComponent<Platform>();
            platform.CollectHexagons();
            platforms.Add(platform);
            numberOfTiles += platform.GetNumberOfHexagons();            
        }
        allTiles = new Hexagon[numberOfTiles];
    }

    /*
     *  Goes through all the hexagons of every platform (which means going through EVERY single hexagon) and add it to the different lists
     *  At the end, give it its individual colour settings
     */
    private void CollectTilesForListsAndColorThem(bool inEditor)
    {
        int tilesCounter = 0;
        for(int i = 0; i < platforms.Count; i++)
        {
            List<Hexagon> platformTiles = platforms[i].GetTilesList();
            for(int k = 0; k < platformTiles.Count; k++)
            {
                Hexagon hexagon = platformTiles[k];
                HexagonBehaviour behaviour = hexagon.GetComponent<HexagonBehaviour>();
                allTiles[tilesCounter] = hexagon;
                tileColors[hexagon] = hexagon.GetColor();                
                bool destroyVFX = true;
                hexagon.SetStandardTile(true);

                if(inEditor && hexagon.GetAudioSource())
                {
                    DestroyImmediate(hexagon.GetAudioSource());
                }
                
                if(hexagon.IsPathTile())
                {
                    SaveHexagonInList(pathTiles, hexagon, hexagon.GetPathNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(pathHexagons);
                }

                if(hexagon.IsCrackedTile())
                {
                    SaveHexagonInList(crackedTiles, hexagon, hexagon.GetCrackedNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetAudio("cracked");
                    hexagon.SetMaterial(crackedHexagons);
                }

                if(hexagon.IsDistractionTile())
                {
                    SaveHexagonInList(distractionTiles, hexagon, hexagon.GetDistractionNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(distractionHexagons);

                    if(!hexagon.GetComponent<HexagonDistraction>()) hexagon.gameObject.AddComponent<HexagonDistraction>();

                    hexagon.GetComponent<HexagonDistraction>().GetStarted(hexagon.GetDistractionNumber(), platforms[i].GetAllPlatformTiles(), 
                                                                            allTiles, tileColors, distractionTiles, hexagon);
                }
                else
                {
                    if(inEditor)
                    {
                        HexagonDistraction distractionScript = hexagon.GetComponent<HexagonDistraction>();
                        if(distractionScript)
                        {                           
                            DestroyImmediate(distractionScript);
                        }                            
                    }
                }

                if(hexagon.IsCheckpointTile())
                {
                    SaveHexagonInList(checkpointTiles, hexagon, hexagon.GetCheckpointNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(checkpointHexagons);
                }

                if(hexagon.IsSpecialTile())
                {                    
                    SaveHexagonInList(specialTiles, hexagon, hexagon.GetSpecialNumber());                    
                    hexagon.SetStandardTile(false);
                    switch (hexagon.GetSpecialNumber()) {
                        case 0: {
                                hexagon.SetMaterial(teleporterHexagons);
                                break;
                            }
                        case 1: {
                                hexagon.SetMaterial(velocityHexagons);
                                break;
                            }
                        case 2: {
                                hexagon.SetMaterial(jumpPadHexagons);
                                break;
                            }
                        case 3: {
                                hexagon.SetMaterial(loosingHexagons);
                                break;
                            }   
                        defaut: {
                                hexagon.SetMaterial(standardHexagons);
                                break;
                            }
                    }
                    if(!hexagon.GetComponent<HexagonSpecial>()) hexagon.gameObject.AddComponent<HexagonSpecial>();
                    HexagonSpecial specialScript = hexagon.GetComponent<HexagonSpecial>();                
                    specialScript.GetStarted(specialTiles, this, hexagon, inEditor);
                    destroyVFX = !specialScript.HasVFX();
                }
                else
                {
                    if(inEditor)
                    {
                        HexagonSpecial specialScript = hexagon.GetComponent<HexagonSpecial>();
                        if(specialScript)
                        {                           
                           DestroyImmediate(specialScript);
                        }                                                    
                    }
                }

                if(hexagon.IsMovingTile())
                {
                    SaveHexagonInList(movingTiles, hexagon, hexagon.GetMovingNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(movingHexagons);
                    if(!hexagon.GetComponent<HexagonMovingTiles>()) hexagon.gameObject.AddComponent<HexagonMovingTiles>();                    
                }
                else
                {
                    if(inEditor)
                    {
                        HexagonMovingTiles movingTilesScript = hexagon.GetComponent<HexagonMovingTiles>();
                        if(movingTilesScript)
                        {
                            DestroyImmediate(movingTilesScript);
                        }                            
                    }
                }

                if(hexagon.IsStartingTile())
                {
                    SaveHexagonInList(startingTiles, hexagon, hexagon.GetStartingNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(startingHexagons);
                    hexagon.SetVisualEffect("StartingVFX", inEditor);
                    destroyVFX = false;
                }

                if(hexagon.IsWinningTile())
                {
                    SaveHexagonInList(winningTiles, hexagon, hexagon.GetWinningNumber());
                    hexagon.SetStandardTile(false);
                    hexagon.SetMaterial(winningHexagons);
                    hexagon.SetVisualEffect("WinningVFX", inEditor);
                    destroyVFX = false;
                }

                if(hexagon.IsStandardTile())
                {
                    standardTiles.Add(hexagon);
                    hexagon.SetMaterial(standardHexagons);
                    if(inEditor && hexagon.GetComponent<AudioSource>())
                    {
                        DestroyImmediate(hexagon.GetComponent<AudioSource>());
                    }
                }

                if(inEditor && hexagon.GetComponent<AudioSource>())
                {
                    if(hexagon.GetComponent<AudioSource>().clip == null)
                    {
                        DestroyImmediate(hexagon.GetComponent<AudioSource>());
                    }
                }

                if(destroyVFX && hexagon.GetVisualEffect()) hexagon.DestroyVisualEffect(inEditor);

                Color[] getAllTouchingColors = this.GetComponent<TileColorsTouching>().GiveColorSet(); // get all Colors when the ball for touching and leaving a hexagon
                behaviour.SetColors(getAllTouchingColors); // give current hexagon the set, in order to get its individual color settings
                tilesCounter++;
            }
        }        
    }

    public void SaveHexagonInList(Dictionary<int, List<Hexagon>> tiles, Hexagon hexagon, int index)
    {
        if(tiles.TryGetValue(index, out List<Hexagon> hexagonList)) // if there is already a list at the specified index (key), then add the hexagon to it
        {
            hexagonList.Add(hexagon);
        }
        else                            // otherwise create a new list, add the hexagon to it and add the new list into the dictionary at the specified index (key)
        {
            List<Hexagon> newHexagonList = new List<Hexagon>();
            newHexagonList.Add(hexagon);
            tiles.Add(index, newHexagonList);
        }        
    }

    public void DeleteHexagonInList(Dictionary<int, List<Hexagon>> tiles, Hexagon hexagon)
    {
        int sizeOfDictionary = tiles.Count;
        for(int i = 0; i < sizeOfDictionary; i++)
        {
            if(tiles.TryGetValue(i, out List<Hexagon> hexagonList))
            {
                hexagonList.Remove(hexagon);
            }
            else
            {
                sizeOfDictionary++;
            }
        }
    }
    

    /* ------------------------------ GETTER METHODS BEGIN ------------------------------  */
    /*
     *  Return the searched platform, otherwise return null
     */
    public Platform GetPlatform(string platformName)
    {
        PrepareLists();
        ClearEverything();        
        CollectPlatforms();        
        for(int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i].name == platformName)
            {
                return platforms[i];
            }
        }
        return null;
    }

    /*
     *  Get all standardTiles
     */

    public List<Hexagon> GetStandardTiles()
    {        
        return standardTiles;
    }

    public Dictionary<int, List<Hexagon>> GetStartingTiles()
    {        
        return startingTiles;
    }

    public Dictionary<int, List<Hexagon>> GetCrackedTiles()
    {        
        return crackedTiles;
    }

    public Dictionary<int, List<Hexagon>> GetMovingTiles()
    {        
        return movingTiles;
    }

    public Dictionary<int, List<Hexagon>> GetPathTiles()
    {        
        return pathTiles;
    }

    public Dictionary<int, List<Hexagon>> GetWinningTiles()
    {        
        return winningTiles;
    }

    public Dictionary<int, List<Hexagon>> GetCheckpointTiles()
    {        
        return checkpointTiles;
    }

    public Dictionary<int, List<Hexagon>> GetDistractionTiles()
    {        
        return distractionTiles;
    }

    public Dictionary<int, List<Hexagon>> GetSpecialTiles()
    {        
        return specialTiles;
    }
    public Hexagon[] GetAllTiles()
    {
        return allTiles;
    } 


    public Hexagon GetSpawnPosition(int startingTileNumber)
    {
        if(startingTiles.TryGetValue(startingTileNumber, out List<Hexagon> startingTilesList))
        {
            return startingTilesList[0]; // Instead of 0, it can be more flexible later, for example with a random number, in case there are more startingTiles for that player
        }
        else
        {
            return null;
        }        
    }


    public int GetNumberOfPlatforms()
    {
        return platforms.Count;
    }

    public List<Platform> GetPlatforms()
    {
        return platforms;
    }


    public Dictionary<int, List<Hexagon>>[] GetAllNonStandardTiles()
    {
        return tileLists;
    }



    /* ------------------------------ DELETION METHOD ------------------------------  */
    /*     
     * Remove a platform from this list of platforms, but it does not destroy it!
     * For destorying a platform, you have to tell the platform itself with DestroyHexagon()!
    */
    public void RemovePlatform(Platform platform)
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i] == platform)
            {                
                platforms.RemoveAt(i);
            }
        }
    }

    public void RemoveHexagonFromAllLists(Hexagon hexagonToDelete)
    {
        if(tileLists == null) 
        {
            CollectTiles(false);
        }
        
        for(int i = 0; i < tileLists.Length; i++)
        {
            int dictionaryEntries = tileLists[i].Count;
            for(int k = 0; k < dictionaryEntries; k++)
            {
                if(tileLists[i].TryGetValue(k, out List<Hexagon> hexagonList)) // if the key is available, then just procceed
                {
                    tileLists[i][k].Remove(hexagonToDelete);
                }
                else
                {
                    dictionaryEntries++;    // if this key doesn't exist, increase the number to keep looking for every available list
                }                
            }
        }
    }

    /*
     *  Empty/prepare all lists
     *  Useful especially in editor mode, where the lists otherwise are not prepared in any way
     */
    private void ClearEverything()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Platform platform = this.transform.GetChild(i).GetComponent<Platform>();
            if(platform) platform.GetTilesList().Clear();
        }

        platforms.Clear();
        

        for(int i = 0; i < tileLists.Length; i++)
        {
            tileLists[i].Clear();
        }        
    }


    /* ------------------------------ EDITOR MODE METHODS ------------------------------  */
    /*
     *  Add a new platform
     */
    public void AddPlatform(Platform platform)
    {
        platforms.Add(platform);
    }


    /* ------------------------------ DEBUGGING METHODS ------------------------------  */
    /*
     *  Print all hexagons and their platforms from any given Dictionary
     */
    private void PrintDictionaryTiles(Dictionary<int, List<Hexagon>> tiles)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            List<Hexagon> tilesList = tiles[i];
            Debug.Log("Current: " + i + " || Number of tiles: " + tilesList.Count);
            
            for(int k = 0; k < tilesList.Count; k++)
            {
                Debug.Log(tilesList[k].GetComponentInParent<Platform>().name + ": " + tilesList[k].name);
            }            
        }
    }

    /*
     *  Used in the UI for testing with the instructor
     */
    public void PrintPathTiles()
    {
        PrintDictionaryTiles(pathTiles);
    }


} // END OF CLASS