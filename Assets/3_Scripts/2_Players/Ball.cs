using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{    
    private Rigidbody rb;
    private HexagonBehaviour occupiedTile;
    private Hexagon lastSpawnPosition;
    private Vector3 firstSpawnPosition;
    private Vector3 lastSpawnOffset;
    private Timer timer;
    private MapSettings settings;
    private GameObject tilesObject;
    private PauseMenu pauseMenu;
    private List<Vector3> positions = new List<Vector3>();
    Dictionary<int, List<Hexagon>> checkpointTiles;
    bool playerMarkedCheckpoints = false;
    bool hasWatchedIntroductionScreen = false;

    private int playerNumber;
    private float loseHeight = -10;
    private int replayPositionCounter = 0;
    
    


    /* ------------------------------ METHODS FOR DIFFERENT STATES BEGINN ------------------------------  */



                    /* --------------- STATUS: SCENE LOADED, PLAYER GETS PREPARED ---------------  */
    /*  
     *  Preparing the ball by saving some of it's components or getting values from the map
     */
    public void GetStarted(int playerNumber)
    {
        firstSpawnPosition = transform.position;
        this.playerNumber = playerNumber;        
        rb = GetComponent<Rigidbody>();
        timer = GetComponentInChildren<Timer>();
        tilesObject = GameObject.Find("Map/Tiles");
        settings = GameObject.Find("Map").GetComponent<MapSettings>();
        lastSpawnOffset = settings.GetSpawnPositionOffset();
        pauseMenu = GetComponentInChildren<PauseMenu>();
        CameraFollow cameraFollow = GetComponentInParent<Players>().GetCamera();
        
        GameObject loseTile = GameObject.Find("Map/UntaggedGameObjects/LoseHeight");
        loseHeight = loseTile.transform.position.y;
 
        StartCoroutine(Introduction(cameraFollow));
    }



                    /* --------------- STATUS: INTRODUCTION, PLAYER HAS TO WAIT ---------------  */
    /*
     *  All the colours of the non-standard tiles will be shown
     *  When all colours have faded, then the game starts
     */
    IEnumerator Introduction(CameraFollow cameraFollow)
    {
        while(!cameraFollow.GetCameraReachedFinalPosition())                                    // wait for the user to finish watching the introduction screen
        {
            yield return new WaitForSeconds(0.2f);
        }


        if(settings.IsIntroductionScreen())                                         // check if the level has a introduction screen to show
        {
            ShowIntroductionScreen();                                               // if so, show it

            while(!hasWatchedIntroductionScreen)                                    // wait for the user to finish watching the introduction screen
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

        /* --------------- DISPLAYING NON-STANDARD TILES ---------------  */
        
        TileColorsIntroduction tileColorsIntroduction = tilesObject.GetComponent<TileColorsIntroduction>(); // Get the script for the colour introduction

        bool chooseCheckPoints = settings.GetNumberOfCheckpoints() > 0;             // are there any checkpoints to choose for this map

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
            checkpointController.GetStarted(settings.GetNumberOfCheckpoints(), checkpointTiles, tiles);   // and get it started

            bool isStoptimeForCheckpoints = settings.GetStoptimeForCheckpoints() > 0;             // get the boolean, if a limited time for choosing checkpoints is set
            if(isStoptimeForCheckpoints)                                            
            {
                timer.Show();                                                       // show the timer
                timer.SetStopWatch(settings.GetStoptimeForCheckpoints());           // and give it the stopwatchtime
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
        
        timer.Show();
        timer.SetStopWatch(3.9f);

        while (!timer.IsStopTimeOver())
        {
            yield return new WaitForSeconds(0.2f);
        }
        
        timer.Disappear();

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
        timer.ShowLastFinishTime();

        Debug.Log("Finish time: " + timer.GetLastFinishTime());
        SceneManager.LoadScene("1_Scenes/Menus/Win");
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
        timer.Disappear();
        StopMovement();
        //GoToSpawnPosition(lastSpawnPosition, lastSpawnOffset);
        SceneManager.LoadScene("1_Scenes/Menus/GameOverMenu");

    }
    
            /* --------------- STATUS: GAME PAUSED ---------------  */

    public void GamePaused()
    { 
       rb.constraints = RigidbodyConstraints.FreezeAll;
       DeactivatePlayerControls();
       timer.Pause();
       timer.Disappear();
    }
            
    public void GameUnpaused()
    {
        StartCoroutine(UnpauseStopwatch(3.9f));
    }

    private IEnumerator UnpauseStopwatch(float seconds)
    {
        timer.SetStopWatch(seconds);
        timer.Show();
        
        while (!timer.IsStopTimeOver())
        {
            yield return new WaitForSeconds(0.00001f);
        }

        timer.Unpause();
        rb.constraints = RigidbodyConstraints.None;
        ActivatePlayerControls();
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
            
        }
        
        else if(hexagon.IsStandardTile() && settings.DoesStandardTilesMeansLosing())
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
        
        else if(hexagon.IsStandardTile() && settings.DoesStandardTilesMeansLosing())
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
     *  Let the player spawn at the last known spawn position
    **/
    public void GoToSpawnPosition()
    {
        GoToSpawnPosition(lastSpawnPosition, lastSpawnOffset);
    }

    /*  
     *  Let the player spawn above the desired tile
    **/
    public void GoToSpawnPosition(Hexagon spawnTile, Vector3 spawnOffset)
    {        
        if(spawnTile != null)
        {
            transform.position = spawnTile.transform.position + spawnOffset;
            lastSpawnPosition = spawnTile;
            lastSpawnOffset = spawnOffset;
        }
        else
        {
            transform.position = firstSpawnPosition;
        }
        
        
    }

    public Hexagon GetLastSpawnPosition()
    {
        return lastSpawnPosition;
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    /*
     *  Deactivates the player attached scripts "Ball" and "AccelerometerMovement". Hence all the effects and manipulations caused by them will be absent.
    **/
    private void DeactivatePlayerControls()
    {
        if(GetComponent<BallControls>()) GetComponent<BallControls>().enabled = false;
        if(GetComponent<ControlsBallFromBehind>()) GetComponent<ControlsBallFromBehind>().enabled = false;
        if(GetComponent<AccelorometerMovement>()) GetComponent<AccelorometerMovement>().enabled = false;        
    }


    /*
     * Activates the player attached scripts "Ball" and "AccelerometerMovement"
     **/
    private void ActivatePlayerControls()
    {
        if(GetComponent<BallControls>()) GetComponent<BallControls>().enabled = true;
        if(GetComponent<ControlsBallFromBehind>()) GetComponent<ControlsBallFromBehind>().enabled = true;
        if(GetComponent<AccelorometerMovement>()) GetComponent<AccelorometerMovement>().enabled = true;
    }


    /* ------------------------------ GETTER METHODS ------------------------------  */
    public int GetPlayerNumber()
    {
        return playerNumber;
    }


} // CLASS END