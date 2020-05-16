using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    List<Hexagon> platformTiles = new List<Hexagon>(); // all tiles of this platform will be found here

    public void AddTile(Hexagon tile)
    {
        platformTiles.Add(tile);
    }

    public List<Hexagon> GetTilesList()
    {
        return platformTiles;
    }

    public int GetNumberOfTiles()
    {
        return platformTiles.Count;
    }

}
