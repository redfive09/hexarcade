using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    List<Hexagon> platformTiles = new List<Hexagon>(); // all tiles of this platform will be found here

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
     * In order to destroy a hexagon tile, we have to remove it first from the list of its platform
     * Parameters: hexagon which should be deleted; inEditor should be "false" if the hexagon should be deleted inside the game - only inEditor mode "true"!
    */
    public void RemoveHexagon(Hexagon hexagon, bool inEditor)
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            if(platformTiles[i] == hexagon)
            {
                Debug.Log(platformTiles.Count);
                platformTiles.RemoveAt(i);

                GameObject hexagonObject = hexagon.transform.gameObject; 
                if(inEditor)
                {
                    DestroyImmediate(hexagonObject);
                }
                else
                {
                    Destroy(hexagonObject);
                }
                Debug.Log(platformTiles.Count);
            }        
        }
    }
}
