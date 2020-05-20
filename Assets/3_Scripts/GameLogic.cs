using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Class purpose: Coordinating all information between the gameObjects
**/
public class GameLogic : MonoBehaviour
{
    // [SerializeField] private GameObject ball;
    // [SerializeField] private GameObject mapGenerator;
    // [SerializeField] private GameObject pathGenerator;
    // [SerializeField] private int levelTime; // time the player has to complete the level
    // [SerializeField] private Text timeDisplay; // text on the canvas to comunicate the time left to the player

    // private Ball player;
    // private Timer timer;
    // private List<Hexagon> levelTiles = new List<Hexagon>(); // Holds all tiles of the current level
    // private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path

    // private float tileColorTime = 0.1f; // time between each tile coloring (used in level intro)

    // // probably also a list for crackedTiles


    // // private Hexagon currentTile; // Not used yet, should be updated every frame


    // // Predefined path for a specific level, should go to another file later
    // private int[,] pathCoordLevel1 =
    // {
    //     {  0,  2},
    //     {  0,  1},
    //     {  1,  0},
    //     {  0, -1},
    //     {  0, -2},
    //     { -1, -3}
    // };


    // /*
    // *   When level starts, it organizes every part of the game in the logical order
    // **/
    // void Start()
    // {
    //     // CreateLevel();
    //     // CreatePlayers();
    //     // PaintTheWorld();
    //     // StartTimers();
    //     // SetDistractorTilesLevel1();
    // }

    // /*
    //  *  Sets the distractor tiles for Level 1.
    //  */
    // void SetDistractorTilesLevel1()
    // {
    //     pathTiles[2].SetIsCrackedTile(true);
    //     pathTiles[3].SetIsMovingTile(true);
    // }

    // void Update()
    // {
    //     if (levelTime <= 0)
    //     {
    //         CancelInvoke("ElapseLevelTime");
    //     }
    // }


    // /*
    //  *  Create all the level relevant stuff, like the map and the path
    // **/
    // void CreateLevel()
    // {
    //     levelTiles = mapGenerator.GetComponent<MapGenerator>().GenerateMap(12, 6, 7, "AllTiles"); // Save tiles of the new generated map, specific information should be outsourced into another file later
    //     pathTiles = pathGenerator.GetComponent<PathGenerator>().GetPathTiles(levelTiles, pathCoordLevel1); // Create a path

    // }

    // /*
    //  *  Written in plural, just in case if we add multiple players later :)
    // **/
    // void CreatePlayers()
    // {
    //     GameObject player1Ball = Instantiate(ball);
    //     player1Ball.name = "Player1";
    //     player = player1Ball.GetComponent<Ball>();
    //     player.GoToSpawnPosition(pathTiles[0]); // First element of the "path" list is the starting tile
    //     // DeactivatePlayerControls();
    //     Invoke("ActivatePlayerControls", pathCoordLevel1.Length * tileColorTime);
    // }






    // /*
    //  *  Starts the elapsing of the time the player has to finish the level after the path was shown. It is coordinated through a time per tile to light up and basically the lenght of the path.
    //  **/
    // void StartTimers()
    // {
    //     InvokeRepeating("ElapseLevelTime", pathCoordLevel1.Length * tileColorTime, 1);
    // }

    // /*
    //  * Counts down level Time by a decement of 1. Set the UI according to the time value;
    //  **/
    // void ElapseLevelTime()
    // {
    //     if (levelTime > 0)
    //     {
    //         levelTime -= 1;
    //         timeDisplay.text = string.Format("Time left:\n{0:G3}", levelTime);
    //     }
    // }

}
