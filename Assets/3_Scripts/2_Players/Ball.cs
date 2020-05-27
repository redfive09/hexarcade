using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{    
    private Rigidbody rb;
    private HexagonBehaviour occupiedTile;
    private Hexagon lastSpawnPosition;
    private Vector3 lastSpawnVector;
    private Timer timer;
    private List<Vector3> positions = new List<Vector3>();
    Dictionary<int, List<Hexagon>> checkpointTiles;
    bool playerMarkedCheckpoints = false;
    bool hasWatchedIntroductionScreen = false;

    private int playerNumber;
    private float loseHeight = -10;
    private int replayPositionCounter = 0;
    private bool standardTilesMeansLosing;
    
    


    /* ------------------------------ METHODS FOR DIFFERENT STATES BEGINN ------------------------------  */



                    /* --------------- STATUS: SCENE LOADED, PLAYER GETS PREPARED ---------------  */
    /*  
     *  Preparing the ball by saving some of it's components or getting values from the map
     */

    public void GetStarted(int playerNumber, int numberOfCheckpoints, float stoptimeForCheckpoints, bool[] boolSettings)
    {
        rb = GetComponent<Rigidbody>();
        timer = this.GetComponentInChildren<Timer>();
        this.playerNumber = playerNumber;

        GameObject loseTile = GameObject.Find("Map/UntaggedGameObjects/LoseHeight");
        loseHeight = loseTile.transform.position.y;


        standardTilesMeansLosing = boolSettings[1];

        StartCoroutine(Introduction(boolSettings[0], numberOfCheckpoints, stoptimeForCheckpoints));
    }



                    /* --------------- STATUS: INTRODUCTION, PLAYER HAS TO WAIT ---------------  */
    /*
     *  All the colours of the non-standard tiles will be shown
     *  When all colours have faded, then the game starts
     */
    IEnumerator Introduction(bool introductionScreen, int numberOfCheckpoints, float stoptimeForCheckpoints)
    {

        if(introductionScreen)                                                      // check if the level has a introduction screen to show
        {
            ShowIntroductionScreen();                                               // if so, show it

            while(!hasWatchedIntroductionScreen)                                    // wait for the user to finish watching the introduction screen
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

        /* --------------- DISPLAYING NON-STANDARD TILES ---------------  */
        GameObject tilesObject = GameObject.Find("Map/Tiles");
        TileColorsIntroduction tileColorsIntroduction = tilesObject.GetComponent<TileColorsIntroduction>(); // Get the script for the colour introduction

        bool chooseCheckPoints = numberOfCheckpoints > 0;                           // are there any checkpoints to choose for this map

        tileColorsIntroduction.DisplayTiles(chooseCheckPoints);                     // display the non-standard tiles

        if(chooseCheckPoints)
        {
            Tiles tiles = tilesObject.GetComponent<Tiles>();                        // get the script of tiles
            checkpointTiles = tiles.GetCheckpointTiles();                           // prepare the place for adding the checkpoints

            while(!tileColorsIntroduction.IsReadyForCheckpoints())                  // once the colours have appeared, it will wait with the fading process
            {
                yield return new WaitForSeconds(0.2f);                              // check regularly if all the colours have appeared
            }
            
            ControlsCheckpoint checkpointController = GetComponent<ControlsCheckpoint>();   // get the controls for choosing the checkpoints
            checkpointController.enabled = true;                                            // enable it
            checkpointController.GetStarted(numberOfCheckpoints, checkpointTiles, tiles);   // and get it started

            bool isStoptimeForCheckpoints = stoptimeForCheckpoints > 0;             // get the boolean, if a limited time for choosing checkpoints is set
            if(isStoptimeForCheckpoints)                                            
            {
                timer.Show();                                                       // show the timer
                timer.SetStopWatch(stoptimeForCheckpoints);                         // and give it the stopwatchtime
            }
            
            while(!playerMarkedCheckpoints &&                                       // check, if the player has marked all available checkpoints
                  !(isStoptimeForCheckpoints && timer.IsStopTimeOver()))            // or if the time for choosing them is over
            {
                playerMarkedCheckpoints = checkpointController.IsFinished();        // update the boolean, if the player has confirmed its choices
                yield return new WaitForSeconds(0.2f);                              // it will check regularly if the player has choosen the checkpoints
            }
            
            timer.Disappear();
            tileColorsIntroduction.Finish();                                        // as soon as the player has choosen the checkpoints, the colours should start fading
            checkpointController.enabled = false;                                   // disable the checkpointController, since we don't need it anymore
        }        
        
        while(!tileColorsIntroduction.IsFinished())                                 // waiting for the tiles to finish fading
        {
            yield return new WaitForSeconds(0.2f);
        }

        GameStarts();

    }


    private void ShowIntroductionScreen()
    {
        // do something here

        hasWatchedIntroductionScreen = true;       // once the introductionScreen has gone, mark the boolean
    }   


                /* --------------- STATUS: GAME STARTED, PLAYER CAN DO SOMETHING ---------------  */
    /*  
     *  This method is called when the game starts
     *  The player gets its controls and the timer will show up
     */
    private void GameStarts()
    {
        ActivatePlayerControls();        
        StartCoroutine(CheckLoseCondition());
    }

    /*  
     *  This method is called when the game starts
     */
    private void PlayerArrviedStartingTile()
    {
        timer.Disappear();        
        Debug.Log("Record to beat: " + timer.GetBestTime());
    }
    
    /*  
     *  This method is called when the game starts
     */
    private void PlayerLeftStartingTile()
    {
        timer.Show();
        timer.StartTiming();
        Debug.Log("Timer started/reseted");
    }


                /* --------------- STATUS: PLAYER WON, PLAYER REACHED A FINISH-TILE ---------------  */
    private void PlayerWon()
    {
        timer.StopTiming();

        Debug.Log("Finish time: " + timer.GetLastFinishTime());

        if(timer.IsNewBestTime())
        {
            Debug.Log("New record");
        }
        else
        {
            Debug.Log("No new record");
        }
    }


                /* --------------- STATUS: PLAYER LOST, PLAYER MET A LOSE CONDITION ---------------  */
    private void PlayerLost()
    {
        GoToSpawnPosition(lastSpawnPosition, lastSpawnVector);
    }


    /* ------------------------------ UPDATING AND WAITING FOR INPUT METHODS ------------------------------  */

    /*       
     *  So far just testing stuff
    **/
    void FixedUpdate()
    {
        // Save current position, not used yet
        positions.Add(transform.position);

        // Start ghost/replay --- JUST A TEST SO FAR ---
        if(Input.GetKeyDown(KeyCode.R))
        {                     
            transform.position = positions[replayPositionCounter];
            replayPositionCounter++;
        }     
    }


    /* ------------------------------ CHECKING AND ANALYSING ENVIRONMENT ------------------------------  */

    /*  
     *  The player checks, if it is standing on a tile; if true, then save the currentTile and tell the current and former tile
    **/
    void OnCollisionEnter(Collision collision)
    {        
        GameObject hexagonObject = collision.gameObject;

        if(hexagonObject.tag == "Tile")
        {
            HexagonBehaviour currentTile = hexagonObject.GetComponent<HexagonBehaviour>();
        
            if(occupiedTile != currentTile)         // Check if the former occupiedTile has changed
            {
                if(occupiedTile != null)            // Prevent a NullReferenceException
                {
                    occupiedTile.GotUnoccupied(this);   // Tell the former occupiedTile, that this ball left
                    AnalyseLeftHexagon(occupiedTile.GetComponent<Hexagon>());
                }

                if(currentTile != null)                 // in case it is a crackableTile, it could be gone already
                {
                    currentTile.GotOccupied(this);      // Tell the currentTile, that this player stands on it
                    occupiedTile = currentTile;         // Save the current tile
                    AnalyseArrivedHexagon(currentTile.GetComponent<Hexagon>());
                }
            }            
        }
    }


    /*  
     *  The player has to check for some specific hexagon types in order to decide what to do next, e. g. starting tiles will start the timer
    **/
    private void AnalyseLeftHexagon(Hexagon hexagon)
    {

        if(hexagon.IsStartingTile())
        {
            PlayerLeftStartingTile();            
        }

        else if(hexagon.IsWinningTile())
        {
            PlayerWon();
        }
        
        else if(hexagon.IsStandardTile() && standardTilesMeansLosing)
        {
            PlayerLost();
        }
    }



    /*  
     *  The player has to check for some specific hexagon types in order to decide what to do next, e. g. winning tiles have to change the state of the player
    **/
    private void AnalyseArrivedHexagon(Hexagon hexagon)
    {
        if(hexagon.IsStartingTile())
        {
            PlayerArrviedStartingTile();
        }
        else if(hexagon.IsWinningTile())
        {
            PlayerWon();
        }
        
        else if(hexagon.IsStandardTile() && standardTilesMeansLosing)
        {
            PlayerLost();
        }
    }


    /* 
     *  This is constantly checking if a lose condition (ball fell to deep) has met
     *  It's packed in a coroutine, so it is not called every single frame -> saves performance
     */
    IEnumerator CheckLoseCondition()
    {
        // Lose condition through falling
        for(;;)
        {
            if(loseHeight > transform.position.y)
            {
                PlayerLost();
            }
            yield return new WaitForSeconds(0.2f);
        }            
    }



    /* ------------------------------ BEHAVIOUR METHODS ------------------------------  */

    /*  
     *  Let the player spawn above the desired tile
    **/
    public void GoToSpawnPosition(Hexagon spawnTile, Vector3 spawnVector)
    {
        transform.position = new Vector3(spawnTile.transform.position.x + spawnVector.x, spawnTile.transform.position.y + spawnVector.y, spawnTile.transform.position.z + spawnVector.z);
        lastSpawnPosition = spawnTile;
        lastSpawnVector = spawnVector;
    }


    /*
     *  Deactivates the player attached scripts "Ball" and "AccelerometerMovement". Hence all the effects and manipulations caused by them will be absent.
    **/
    void DeactivatePlayerControls()
    {
        GetComponent<BallControls>().enabled = false;
        GetComponent<AccelorometerMovement>().enabled = false;
    }


    /*
     * Activates the player attached scripts "Ball" and "AccelerometerMovement"
     **/
    void ActivatePlayerControls()
    {
        GetComponent<BallControls>().enabled = true;
        GetComponent<AccelorometerMovement>().enabled = true;
    }


} // CLASS END