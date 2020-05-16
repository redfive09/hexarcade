using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<Hexagon> levelTiles = new List<Hexagon>(); // Holds all tiles of the current level
    private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path
    private Hexagon startingTile;
    private Hexagon winingTile;


}
