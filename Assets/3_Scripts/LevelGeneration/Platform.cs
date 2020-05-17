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

    public void RemoveHexagonInEditor(Hexagon hexagon)
    {
        for(int i = 0; i < platformTiles.Count; i++)
        {
            // if(platformTiles[i].GetInstanceID() == hexagon.GetInstanceID())
            // {
                if(platformTiles[i] == hexagon)
                {
                    // platformTiles[i]
                    Debug.Log(platformTiles[i].GetInstanceID());
                    Debug.Log(hexagon.GetInstanceID());
                    Debug.Log(hexagon);
                    platformTiles.RemoveAt(i);
                    // DestroyImmediate(hexagon);
                }
            // }
        }
    }

}
