using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    List<Hexagon> platformTiles = new List<Hexagon>(); // all hexagons of this platform will be found here

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
     * Remove a hexagon from its platform, but it does not destroy it!
     * For destorying a hexagon, you have to tell the hexagon itself with DestroyHexagon()!
    */
    public void RemoveHexagon(Hexagon hexagon)
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            if(platformTiles[i] == hexagon)
            {
                platformTiles.RemoveAt(i);
            }
        }
    }
}
