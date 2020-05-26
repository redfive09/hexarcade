using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    
    Tiles tiles;
    Players players;
    
    
    void Start()
    {
        CreateTiles();
        CreatePlayers();
    }

    void CreateTiles()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted();


        GameObject mapGenerator = GameObject.Find("/MapGenerator");
        mapGenerator.SetActive(false);
    }

    void CreatePlayers()
    {
        players = this.transform.GetComponentInChildren<Players>();
        players.GetStarted(tiles.GetStartingTiles(), tiles.GetCheckpointTiles());
    }


/* ------------------------------ EDITOR MODE METHODS ------------------------------  */
    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }


} // END OF CLASS
