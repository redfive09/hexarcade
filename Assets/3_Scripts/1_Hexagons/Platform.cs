using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color color;

    
    [SerializeField] private int pathTiles;
    [SerializeField] private int startingTiles;
    [SerializeField] private int winningTiles;
    [SerializeField] private int checkpointTiles;
    [SerializeField] private int distractionTiles;
    [SerializeField] private int specialTiles;

    List<Hexagon> platformTiles = new List<Hexagon>(); // all hexagons of this platform will be found here

    

    /* ------------------------------ ONLY EDITOR METHODS BEGINN ------------------------------  */
    // Prepares all the standard values of the [SerializeField] for the editor mode
    public void Setup()
    {
        pathTiles = -1;
        startingTiles = -1;
        winningTiles = -1;
        checkpointTiles = -1;
        distractionTiles = -1;
        specialTiles = -1;
    }


    /* ------------------------------ STARTING METHODS BEGINN ------------------------------  */

    /*
     * Each platform goes through each hexagon it's connected to and adds it to the list
    */
    public void CollectHexagons()
    {        
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Hexagon hexagon = this.transform.GetChild(i).GetComponent<Hexagon>();
            platformTiles.Add(hexagon);
        }
    }


    /* ------------------------------ SETTER METHODS BEGINN ------------------------------  */

    /*
     * Changes all the colours of all hexagon tiles
    */
    public void SetColor()
    {
        SetColor(this.color);
        Debug.Log(platformTiles.Count);
    }

    public void SetColor(Color color)
    {
        this.color = color;
        for(int i = 0; i < platformTiles.Count; i++)
        {            
            platformTiles[i].SetColor(color);
        }
    }

    /*
     * Uses the [SerializeField] pathTiles to set all tiles at once in the editor
    */
    public void SetPathPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsPathTile(pathTiles);
        }
    }

    /*
     * Uses the [SerializeField] startingTiles to set all tiles at once in the editor
    */
    public void SetStartingPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsStartingTile(startingTiles);
        }
    }

    /*
     * Uses the [SerializeField] startingTiles to set all tiles at once in the editor
     */
    public void SetWinningPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsWinningTile(winningTiles);
        }
    }
    public void SetDistractionPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsDistractionTile(distractionTiles);
        }
    }
    public void SetCheckpointPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsCheckpointTile(checkpointTiles);
        }
    }

    public void SetSpecialPlatform()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].SetIsSpecialTile(specialTiles);
        }
    }

    public void UntagAllHexagons()
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            platformTiles[i].tag = "Untagged";
        }

        Tiles tiles = GetComponentInParent<Tiles>();
        tiles.GetPlatforms().Remove(this);

        GameObject untaggedGameObjects = GameObject.Find("Map/UntaggedGameObjects");
        transform.parent = untaggedGameObjects.transform;
    }

    public void AddHexagon(Hexagon hexagon)
    {
        platformTiles.Add(hexagon);
    }


    /* ------------------------------ GETTER METHODS BEGINN ------------------------------  */


    public List<Hexagon> GetTilesList()
    {
        return platformTiles;
    }

    public int GetNumberOfHexagons()
    {
        return platformTiles.Count;
    }



    /* ------------------------------ DELETION METHODS ------------------------------  */
    /*     
     * Remove a hexagon from its platform, but it does not destroy it!
     * For destorying a hexagon, you have to tell the hexagon itself with DestroyHexagon()!
    */
    public void RemoveHexagon(Hexagon hexagon, bool inEditor)
    {
        Tiles tiles = GetComponentInParent<Tiles>();
        tiles.RemoveHexagonFromAllLists(hexagon); // delete hexagon from all lists!

        for(int i = 0; i < platformTiles.Count; i++)
        {
            if(platformTiles[i] == hexagon)
            {
                platformTiles.RemoveAt(i);
            }
        }
    }
    
    public void DestroyPlatform(bool inEditor)
    {
        // First, delete all its hexagons properly
        for(int i = 0; i < platformTiles.Count; i++)
        {          
            platformTiles[i].DestroyHexagon(inEditor, 0);
        }

        Tiles tiles = GetComponentInParent<Tiles>();  // first tell the list of all platforms to remove it!
        
        if(tiles != null)
        {
            tiles.RemovePlatform(this);
        }        

        if(inEditor)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
} // END OF CLASS
