using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private int startingTiles;
    [SerializeField] private int winningTiles;

    List<Hexagon> platformTiles = new List<Hexagon>(); // all hexagons of this platform will be found here
    

    /* ------------------------------ ONLY EDITOR METHODS BEGINN ------------------------------  */
    // Prepares all the standard values of the [SerializeField] for the editor mode
    public void Setup()
    {
        startingTiles = -1;
        winningTiles = -1;
    }

    /* ------------------------------ STARTING METHODS BEGINN ------------------------------  */
    public void GetStarted()
    {
        CollectHexagons();
    }

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
        CollectHexagons();
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
        for(int i = 0; i < platformTiles.Count; i++)
        {
            if(platformTiles[i] == hexagon)
            {
                platformTiles.RemoveAt(i);
            }
        }

        // In case last hexagon of a platform gets removed, then destroy its platform as well
        if(platformTiles.Count == 0)
        {
            DestroyPlatform(inEditor);
        }
    }
    
    public void DestroyPlatform(bool inEditor)
    {
        Tiles tiles = GetComponentInParent<Tiles>();  // first tell the list of all platforms to remove it!
        tiles.RemovePlatform(this);

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
