using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int startingTiles;
    [SerializeField] private int winningTiles;

    List<Hexagon> platformTiles = new List<Hexagon>(); // all hexagons of this platform will be found here
    

    // Prepares all the standard values of the [SerializeField] for the editor mode
    public void Setup()
    {
        startingTiles = -1;
        winningTiles = -1;
    }

    public void AddHexagon(Hexagon hexagon)
    {
        platformTiles.Add(hexagon);
    }

    public List<Hexagon> GetTilesList()
    {
        return platformTiles;
    }

    public int GetNumberOfHexagons()
    {
        return platformTiles.Count;
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
