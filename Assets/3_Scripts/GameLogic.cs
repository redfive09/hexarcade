using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Class purpose: Coordinating all information between the gameObjects
**/
public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject mapGenerator;
    [SerializeField] private GameObject pathGenerator;
    [SerializeField] private int levelTime; // time the player has to complete the level
    [SerializeField] private Text timeDisplay; // text on the canvas to comunicate the time left to the player

    private Ball player;
    private Timer timer;
    private List<Hexagon> levelTiles = new List<Hexagon>(); // Holds all tiles of the current level
    private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path
    
    private float tileColorTime = 0.1f; // time between each tile coloring (used in level intro)

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
        
        
        PaintTheWorld();
        SetDistractorTilesLevel1();
    }

    void Update()
    {
        if (levelTime <= 0)
        {
            CancelInvoke("ElapseLevelTime");
        }
    }

    /*
     *  Sets the distractor tiles for Level 1.
     */
    void SetDistractorTilesLevel1()
    {
        pathTiles[2].SetIsCrackedTile(true);
        pathTiles[3].SetIsMovingTile(true);
        StartTimers();
    }







    /*
     * Activates the player attached scripts "Ball" and "AccelerometerMovement"
     **/
    void ActivatePlayerControls()
    {
        player.GetComponent<Ball>().enabled = true;
        player.GetComponent<AccelorometerMovement>().enabled = true;
    }

    /*
     *  In this method, all the colours and effects have its place
    **/
    void PaintTheWorld()
    {
        // Specific color should go later to a central place for all settings
        SetColorOfTilesList(Color.cyan, levelTiles);
        StartCoroutine(SetPathColor(Color.yellow, pathTiles, tileColorTime));  // Since the method returns an IEnumerator it has to be calles with startCoroutine

        Hexagon winningTile = pathTiles[pathTiles.Count - 1];       // winningTile is the last element in the pathTiles list
        winningTile.SetColor(Color.green);                          // Give winningTile a different colour
        winningTile.SetIsWinningTile(0);                            // Generally should this not be set here, it just stays for now
    }

    /*
     *  Starts the elapsing of the time the player has to finish the level after the path was shown. It is coordinated through a time per tile to light up and basically the lenght of the path.
     **/
    void StartTimers()
    {
        InvokeRepeating("ElapseLevelTime", pathCoordLevel1.Length * tileColorTime, 1);
    }

    /*
     * Counts down level Time by a decement of 1. Set the UI according to the time value;
     **/
    void ElapseLevelTime()
    {
        if (levelTime > 0)
        {
            levelTime -= 1;
            timeDisplay.text = string.Format("Time left:\n{0:G3}", levelTime);
        }
    }


    /*  This method will make the colour of the path
     *  Works : tiles are colored in with a delay
    **/
    IEnumerator SetPathColor(Color color, List<Hexagon> tiles, float time)
    {
        for(int i = 0; i < tiles.Count-1; i++) // Spare the winningTile, that's why we calculate "tiles.Count -1"
        {
            tiles[i].GetComponent<Hexagon>().SetColor(color);
            yield return new WaitForSeconds(time); //wait 2 seconds before continuing with the loop
            // Tutorial: https://answers.unity.com/questions/1604527/instantiate-an-array-of-gameobjects-with-a-time-de.html
        }
        yield return new WaitForSeconds(time); //wait the specified seconds in time after entire path has lit up
        StartCoroutine(MakePathDisappear(Color.cyan, pathTiles, 1f)); //start the disappering backwards
    }

    /*
    * Method goes trough the list of path tiles in the opposite ordner and colors them in the color of all other tiles,
    * in this case: cyan
    */
    IEnumerator MakePathDisappear(Color color, List<Hexagon> tiles, float time)
    {
        for(int i = tiles.Count-2 ; i > 0 ; i--) // Spare the winningTile, that's why we calculate "tiles.Count -2"
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
