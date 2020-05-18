using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    private List<Platform> platforms = new List<Platform>(); // Holds all platforms of the current level
    // private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path

    private Dictionary<int, List<Hexagon>> pathTiles = new Dictionary<int, List<Hexagon>>();
    private Dictionary<int, List<Hexagon>> startingTiles = new Dictionary<int, List<Hexagon>>();
    private Dictionary<int, List<Hexagon>> winningTiles = new Dictionary<int, List<Hexagon>>();
    private Dictionary<int, List<Hexagon>> checkpointTiles = new Dictionary<int, List<Hexagon>>();


    /* ------------------------------ STARTING METHODS BEGINN ------------------------------  */
    public void GetStarted()
    {
        CollectTiles();        
        this.GetComponent<TileColors>().GetStarted();
        // PrintDictionaryTiles(startingTiles);
    }

    public void CollectTiles()
    {
        CollectPlatforms();
        CollectTilesForListsAndColorThem();
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
     *  Goes through all the tiles of every platform
     */
    private void CollectTilesForListsAndColorThem()
    {   
        for(int i = 0; i < platforms.Count; i++)
        {            
            List<Hexagon> platformTiles = platforms[i].GetTilesList();
            for(int k = 0; k < platformTiles.Count; k++)
            {                
                Hexagon hexagon = platformTiles[k];

                if(hexagon.IsPathTile())
                {
                    SaveHexagonInList(pathTiles, hexagon, hexagon.GetPathNumber());
                }

                if(hexagon.IsStartingTile())
                {
                    SaveHexagonInList(startingTiles, hexagon, hexagon.GetStartingNumber());
                }

                if(hexagon.IsWinningTile())
                {
                    SaveHexagonInList(winningTiles, hexagon, hexagon.GetWinningNumber());
                }

                if(hexagon.IsCheckpointTile())
                {
                    SaveHexagonInList(checkpointTiles, hexagon, hexagon.GetCheckpointNumber());
                }

                this.GetComponent<TileColors>().GiveColors(hexagon.GetComponent<HexagonBehaviour>());
            }
        }
    }

    private void SaveHexagonInList(Dictionary<int, List<Hexagon>> tiles, Hexagon hexagon, int index)
    {
        if(tiles.TryGetValue(index, out List<Hexagon> hexagonList))
        {
            hexagonList.Add(hexagon);
        }
        else
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
     *  Get all startingTiles
     */
    public Dictionary<int, List<Hexagon>> GetStartingTiles()
    {        
        return startingTiles;
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

    /* ------------------------------ SETTER METHODS BEGIN ------------------------------  */




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
        for(int i = 0; i < pathTiles.Count; i++)
        {            
            pathTiles[i].Remove(hexagonToDelete);
        }

        for(int i = 0; i < startingTiles.Count; i++)
        {            
            startingTiles[i].Remove(hexagonToDelete);
        }

        for(int i = 0; i < winningTiles.Count; i++)
        {            
            winningTiles[i].Remove(hexagonToDelete);
        }

        for(int i = 0; i < checkpointTiles.Count; i++)
        {            
            checkpointTiles[i].Remove(hexagonToDelete);
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
