using System.Collections;
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
    private List<Hexagon> standardTiles = new List<Hexagon>();  // list of all tiles without any special purpose



    /* ------------------------------ STARTING METHODS BEGINN ------------------------------  */
    public void GetStarted()
    {        
        CollectTiles();
        GetComponent<TileColorsIntroduction>().GetStarted();
        GetComponent<TilesAllCrackables>().GetStarted(crackedTiles);        
    }

    public void CollectTiles()
    {
        PrepareLists();
        CollectPlatforms();
        CollectTilesForListsAndColorThem();
        
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

    void CollectPlatforms()
    {        
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Platform platform = this.transform.GetChild(i).GetComponent<Platform>();
            platform.CollectHexagons();
            platforms.Add(platform);
        }
    }

    /*
     *  Goes through all the hexagons of every platform (which means going through EVERY single hexagon) and add it to the different lists
     *  At the end, give it its individual colour settings
     */
    private void CollectTilesForListsAndColorThem()
    {   
        for(int i = 0; i < platforms.Count; i++)
        {
            List<Hexagon> platformTiles = platforms[i].GetTilesList();
            for(int k = 0; k < platformTiles.Count; k++)
            {                
                Hexagon hexagon = platformTiles[k];

                if(hexagon.IsCrackedTile())
                {
                    SaveHexagonInList(crackedTiles, hexagon, hexagon.GetCrackedNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsPathTile())
                {
                    SaveHexagonInList(pathTiles, hexagon, hexagon.GetPathNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsDistractionTile())
                {
                    SaveHexagonInList(distractionTiles, hexagon, hexagon.GetDistractionNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsCheckpointTile())
                {
                    SaveHexagonInList(checkpointTiles, hexagon, hexagon.GetCheckpointNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsSpecialTile())
                {
                    SaveHexagonInList(specialTiles, hexagon, hexagon.GetSpecialTileNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsMovingTile())
                {
                    SaveHexagonInList(movingTiles, hexagon, hexagon.GetMovingNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsStartingTile())
                {
                    SaveHexagonInList(startingTiles, hexagon, hexagon.GetStartingNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsWinningTile())
                {
                    SaveHexagonInList(winningTiles, hexagon, hexagon.GetWinningNumber());
                    hexagon.SetStandardTile(false);
                }

                if(hexagon.IsStandardTile())
                {
                    standardTiles.Add(hexagon);
                }

                Color[] getAllTouchingColors = this.GetComponent<TileColorsTouching>().GiveColorSet(); // get all Colors when the ball for touching and leaving a hexagon
                hexagon.GetComponent<HexagonBehaviour>().SetColors(getAllTouchingColors); // give current hexagon the set, in order to get its individual color settings
            }
        }
    }

    private void SaveHexagonInList(Dictionary<int, List<Hexagon>> tiles, Hexagon hexagon, int index)
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

    /* ------------------------------ GETTER METHODS BEGIN ------------------------------  */
    /*
     *  Return the searched platform, otherwise return null
     */
    public Platform GetPlatform(string platformName)
    {
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

    public int GetNumberOfPlatforms()
    {
        return platforms.Count;
    }

    public List<Platform> GetPlatforms()
    {
        return platforms;
    }

    public int GetNumberOfPathTiles(int player)
    {
        return pathTiles[player].Count;
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
        for(int i = 0; i < tileLists.Length; i++)
        {
            for(int k = 0; k < tileLists[i].Count; k++)
            {
                tileLists[i][k].Remove(hexagonToDelete);
            }
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