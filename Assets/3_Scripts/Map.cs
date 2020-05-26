using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    
    [SerializeField] private bool introductionScreen;           // 0
    [SerializeField] private bool standardTilesMeansLosing;     // 1

    private Tiles tiles;
    private Players players;
    
    
    void Start()
    {
        CreateTiles();
        CreatePlayers();
    }

    private void CreateTiles()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted();


        GameObject mapGenerator = GameObject.Find("/MapGenerator");
        mapGenerator.SetActive(false);
    }

    private void CreatePlayers()
    {
        bool[] boolSettings = new bool[] {
            introductionScreen,
            standardTilesMeansLosing
        };

        players = this.transform.GetComponentInChildren<Players>();
        players.GetStarted(tiles.GetStartingTiles(), boolSettings);
    }


/* ------------------------------ EDITOR MODE METHODS ------------------------------  */
    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }


} // END OF CLASS
