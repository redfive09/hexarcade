using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Tiles tiles;

    void Start()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted();
    }

    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }

}
