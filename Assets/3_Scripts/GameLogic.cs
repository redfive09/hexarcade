using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Class purpose: Coordinating all information between the gameObjects
**/
public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject mapGenerator;
    [SerializeField] private GameObject pathGenerator;

    private Ball player;
    private List<Hexagon> levelTiles = new List<Hexagon>(); // Holds all tiles of the current level
    private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path
    // probably also a list for crackedTiles


    // private Hexagon currentTile; // Not used yet, should be updated every frame


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
        CreatePlayers();
        PaintTheWorld();
    }


    /*
     *  Create all the level relevant stuff, like the map and the path
    **/
    void CreateLevel()
    {
        levelTiles = mapGenerator.GetComponent<MapGenerator>().GenerateMap(12, 6, 7, "AllTiles"); // Save tiles of the new generated map, specific information should be outsourced into another file later
        pathTiles = pathGenerator.GetComponent<PathGenerator>().GetPathTiles(levelTiles, pathCoordLevel1); // Create a path

    }

    /*
     *  Written in plural, just in case if we add multiple players later :)
    **/
    void CreatePlayers()
    {
        GameObject player1Ball = Instantiate(ball);
        player1Ball.name = "Player1";
        player = player1Ball.GetComponent<Ball>();
        player.GoToSpawnPosition(pathTiles[0]); // First element of the "path" list is the starting tile

    }


    /*
     *  In this method, all the colours and effects have its place
    **/
    void PaintTheWorld()
    {
        // Specific color should go later to a central place for all settings
        SetColorOfTilesList(Color.cyan, levelTiles);
        StartCoroutine(SetPathColor(Color.yellow, pathTiles, 2f)); //since the method returns an IEnumerator it has to be calles with startCoroutine

    }


    /*  This method will make the colour of the path
     *  Works : tiles are colored in with a delay
     *
    **/
    IEnumerator SetPathColor(Color color, List<Hexagon> tiles, float time)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Hexagon>().SetColor(color);
            yield return new WaitForSeconds(time); //wait 2 seconds before continuing with the loop
            // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
        }
        yield return new WaitForSeconds(time); //wait two seconds after entire path has lit up
        StartCoroutine(MakePathDisappear(Color.cyan, pathTiles, 1f)); //start the disappering backwards
    }

    /*
    * Method goes trough the list of path tiles in the opposite ordner and colors them in the color of all other tiles,
    * in this case: cyan
    */
    IEnumerator MakePathDisappear(Color color, List<Hexagon> tiles, float time)
    {
        for(int i = tiles.Count -1 ; i > 0 ; i--)
        {
            tiles[i].GetComponent<Hexagon>().SetColor(color);
            yield return new WaitForSeconds(time); //wait 2 seconds before continuing with the loop
            // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
        }
    }

    /*
     *  This method goes through all tiles and changes their colors
    **/
    void SetColorOfTilesList(Color color, List<Hexagon> tiles)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Hexagon>().SetColor(color);
        }
    }

}
