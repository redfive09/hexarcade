using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
*  Class purpose: Coordinating all information between the gameObjects
**/ 
public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject MapGenerator;
    [SerializeField] GameObject PathGenerator;


    private List<GameObject> levelTiles = new List<GameObject>(); // Holds all tiles of the current level
    private List<GameObject> path = new List<GameObject>(); // Holds all tiles of the current path


    // Predefined path for a specific level, should go to another file later
    private int[,] pathCoordLevel1 = 
    {
        {  0,  2},
        {  0,  1},
        {  1,  0},
        {  0, -1},
        {  0, -2},
        { -1, -3}
    };


    /*  
    *   When level starts, it organizes every part of the game in the logical order
    **/ 
    void Start()
    {
        CreateLevel();
        SpawnBall();
    }

    /*  
     *  Create all the level relevant stuff, like the map and the path
    **/
    void CreateLevel()
    {
        levelTiles = MapGenerator.GetComponent<MapGenerator>().GenerateMap(12, 6, 7, "AllTiles"); // Save tiles of the new generated map, specific information should be outsourced into another file later
        path = PathGenerator.GetComponent<PathGenerator>().GetPathTiles(levelTiles, pathCoordLevel1); // Create a path

    }

    /*  Let the Ball spawn at the desired position
     *  Not working yet, eventhough the startingTile is correct
    **/
    void SpawnBall()
    {
        GameObject startingTile = path[0]; // First element of the "path" list is the starting tile
        Ball.GetComponent<BallMover>().GoToSpawnPosition(startingTile);
        Debug.Log(startingTile);
    }

   
}
