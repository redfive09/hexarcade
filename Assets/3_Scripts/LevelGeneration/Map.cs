using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Tiles tiles;

    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }

}
