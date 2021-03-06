﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{    
    private const float TIME_TO_CHECK_AGAIN = 0.06f; // Waiting time for Coroutines
    private const float CAMERA_TIME_ALIGNMENT = 0.75f;

    private Rigidbody rb;
    private CameraFollow cameraFollow;
    private SkipButton skipButton;
    private HexagonBehaviour occupiedTile = null;
    private Hexagon lastSpawnPosition;
    private Vector3 firstSpawnPosition;
    private Vector3 lastSpawnOffset;
    private Timer timer;
    private MapSettings settings;
    private Tiles tiles;
    private TileColorsIntroduction tileColorsIntroduction;
    private TutorialManager tutorialManager;
    private GameObject accelerometerInformation;    
    Dictionary<int, List<Hexagon>> checkpointTiles;    
    private bool hasWatchedIntroductionScreen = false;
    private bool controlOn = false;
    private bool wasControlOn = false;    
    private GameObject distractionAtCanvas = null;
    private int playerNumber;
    private float loseHeight = -10;
    private Vector3 rememberVelocity;
    

    // private int replayPositionCounter = 0;
    // private List<Vector3> positions = new List<Vector3>();
    
    


    /* ------------------------------ METHODS FOR DIFFERENT STATES BEGINN ------------------------------  */



                    /* --------------- STATUS: SCENE LOADED, PLAYER GETS PREPARED ---------------  */
    /*  
     *  Preparing the ball by saving some of it's components or getting values from the map
     */
    
    void Start()
    {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    public void GetStarted(int playerNumber)
    {
        Game.hasStarted = false;
        Game.isPaused = false;
        
        firstSpawnPosition = transform.position;
        this.playerNumber = playerNumber;        
        rb = GetComponent<Rigidbody>();
        timer = GetComponentInChildren<Timer>();
        tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();
        tileColorsIntroduction = tiles.GetComponent<TileColorsIntroduction>(); // Get the script for the colour introduction
        settings = GameObject.Find("Map").GetComponent<MapSettings>();
        lastSpawnOffset = settings.GetSpawnPositionOffset();        
        cameraFollow = GetPlayerCamera().GetComponent<CameraFollow>();
        skipButton = GetComponentInChildren<SkipButton>();
        tutorialManager = GetComponentInChildren<TutorialManager>(true);
        accelerometerInformation = GetComponentInChildren<AccelorometerReading>(true).transform.parent.gameObject;

        GameObject loseTile = GameObject.Find("Map/UntaggedGameObjects/LoseHeight");
        loseHeight = loseTile.transform.position.y;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        timer.GetReady();
        SceneTransitionValues.record = timer.GetBestTime();
        SceneTransitionValues.alreadyEnteredEndScreen = false;

        if(SceneTransitionAudio.Instance != null)
        {
            SceneTransitionAudio.Instance.gameObject.GetComponent<AudioSource>().Stop();
        }
 
        StartCoroutine(Introduction(cameraFollow, skipButton));
    }



                    /* --------------- STATUS: INTRODUCTION, PLAYER HAS TO WAIT ---------------  */
    /*
     *  All the colours of the non-standard tiles will be shown
     *  When all colours have faded, then the game starts
     */
    IEnumerator Introduction(CameraFollow cameraFollow, SkipButton skipButton)
    {
        while(!cameraFollow.GetCameraReachedFinalPosition())                        // wait for the user to finish watching the introduction screen
        {
            yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
        }


        // if(settings.IsIntroductionScreen())                                         // check if the level has a introduction screen to show
        // {
            // tutorialManager.gameObject.SetActive(true);
          /*  ShowIntroductionScreen();                                               // if so, show it

            while(!hasWatchedIntroductionScreen)                                    // wait for the user to finish watching the introduction screen
            {
                
                yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
            }*/
        // }

        /* --------------- DISPLAYING NON-STANDARD TILES ---------------  */
                

        bool chooseCheckPoints = settings.GetNumberOfCheckpoints() > 0;             // are there any checkpoints to choose for this map
        ShowNonStandardTiles(chooseCheckPoints);        
        
        if(chooseCheckPoints)
        {
            while(!tileColorsIntroduction.IsReadyForCheckpoints())                  // once the colours have appeared, it will wait with the fading process
            {
                yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);               // check regularly if all the colours have appeared
            }

            ControlsCheckpoint checkpointController = GetComponent<ControlsCheckpoint>();   // get the controls for choosing the checkpoints
            bool isStoptimeForCheckpoints = settings.GetStoptimeForCheckpoints() > 0;       // get the boolean, if a limited time for choosing checkpoints is set
            SelectCheckpoints(checkpointController, isStoptimeForCheckpoints);
                                    
            while(!(isStoptimeForCheckpoints && timer.IsStopTimeOver()) &&          // there is a stopwatch for choosing checkpoints and it's not over yet and
                  !skipButton.IsButtonPressed())                                    // the skipButton, for confirming the player's selections, has not been pressed yet
            {                
                yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);               // it will check regularly if the player has choosen the checkpoints
            }
            
            FinishedSelectingCheckpoints(checkpointController);
            
            while(!cameraFollow.GetCameraReachedFinalPosition())                    // wait for the camera to be back in position
            {
                if(skipButton.IsButtonPressed())
                {
                    cameraFollow.GoToTargetInstantly();
                }
                yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
            }
            
            FinishShowingNonStandardTiles();
        }
                
        while(!tileColorsIntroduction.IsFinished())                                 // waiting for the tiles to finish fading
        {
            yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
        }

        WaitForCameraToBeInPlayingPosition();

        while(!cameraFollow.GetCameraReachedFinalPosition())                        // wait for the camera to be back in position
        {
            if(skipButton.IsButtonPressed())
            {
                cameraFollow.GoToTargetInstantly();
            }
            yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
        }
                
        SetCountdownForPlaying();

        while (!timer.IsStopTimeOver() && !skipButton.IsButtonPressed())            // as soon as the stoptime is over or the skipbutton pressed, the game will start
        {
            yield return new WaitForSeconds(TIME_TO_CHECK_AGAIN);
        }

        PrepareGameStart();        
        GameStarts();                                                               // start the game
    }


    private void ShowIntroductionScreen()
    {
        // do something here

        hasWatchedIntroductionScreen = true;       // once the introductionScreen has gone, mark the boolean
    }
    
    private void ShowNonStandardTiles(bool chooseCheckPoints)
    {
        cameraFollow.ChangeCameraSettings(false, false, CAMERA_TIME_ALIGNMENT);                 // change settings for colour introductions
        tileColorsIntroduction.DisplayTiles(chooseCheckPoints, skipButton, cameraFollow);       // display the non-standard tiles
    }

    private void SelectCheckpoints(ControlsCheckpoint checkpointController, bool isStoptimeForCheckpoints)
    {
        checkpointTiles = tiles.GetCheckpointTiles();                           // prepare the place for adding the checkpoints
        skipButton.ResetForCheckpoints();                                       // The button will reset its booleans and change its text        
        cameraFollow.enabled = false;                                           // deactivate the camera script            
        GetPlayerCamera().orthographic = true;                                  // change the projection of the camera
        checkpointController.enabled = true;                                    // enable the checkpointController
        checkpointController.GetStarted(settings.GetNumberOfCheckpoints(),      // and get it started
            checkpointTiles, tiles, GetPlayerCamera(), GetComponentInChildren<CheckpointCounter>()/* , GetComponentInChildren<CheckpointCounter>() */);

        if(isStoptimeForCheckpoints)                                            
        {
            timer.Show();                                                       // show the timer
            timer.SetStopWatch(settings.GetStoptimeForCheckpoints());           // and give it the stopwatchtime
        }
    }

    private void FinishedSelectingCheckpoints(ControlsCheckpoint checkpointController)
    {
        timer.Disappear();                                                      // let the timer disappear
        skipButton.Reset();                                                     // reset the skipButton
        GetComponentInChildren<CheckpointCounter>().Disappear();                // make the checkpointCounter disappear
        checkpointController.enabled = false;                                   // disable the checkpointController, since we don't need it anymore
        GetPlayerCamera().orthographic = false;                                 // change the projection of the camera
        cameraFollow.enabled = true;                                            // enable the player camera again
        cameraFollow.ChangeCameraSettings(false, true, CAMERA_TIME_ALIGNMENT);  // change settings for a nice effect, while going back to the last target
        cameraFollow.GetBackInPosition();                                       // go back to target
    }

    private void FinishShowingNonStandardTiles()
    {
        skipButton.Reset();
        cameraFollow.ChangeCameraSettings(false, false, CAMERA_TIME_ALIGNMENT); // change settings for colour introductions
        tileColorsIntroduction.Finish();                                        // as soon as the player has choosen the checkpoints and the camera is back, the colours start fading
        cameraFollow.ResetCameraRotation();                                     // get original rotation back
    }

    private void WaitForCameraToBeInPlayingPosition()
    {        
        cameraFollow.GetBackInPosition();                                       // go back to target
        skipButton.Reset();                                                     // reset the skipButton        
    }

    private void SetCountdownForPlaying()
    {
        cameraFollow.SetTarget(transform);                                      // focus the camera back to the player        
        timer.Show();                                                           // the timer appears now
        timer.SetStopWatch(3);                                                  // it counts down before the game starts
        skipButton.Reset();                                                     // skipbutton can be used to skip the countdown
    }

    private void PrepareGameStart()
    {
        cameraFollow.ResetCameraSettings();                                     // set the original camera settings        
        skipButton.gameObject.SetActive(false);                                 // disappear the skipbutton
        accelerometerInformation.SetActive(true);                               // activate the accelerometerGUI elements
        
        if(settings.IsIntroductionScreen())                                     // check if the level has a introduction screen to show
        {
            tutorialManager.gameObject.SetActive(true);        
        }
    }


                /* --------------- STATUS: GAME STARTED, PLAYER CAN DO SOMETHING ---------------  */
    /*  
     *  This method is called when the game starts
     *  The player gets its controls and the timer will show up
     */
    private void GameStarts()
    {        
        Game.hasStarted = true;
        rb.constraints = RigidbodyConstraints.None;
        ActivatePlayerControls();
        StartCoroutine(CheckLoseCondition());
    }

    /*  
     *  This method is called when the game starts
     */
    public void ArrviedStartingTile()
    {
        if(!timer.IsStopwatchMode() || skipButton.IsButtonPressed())
        {
            timer.Disappear();
            skipButton.Reset();
        }
    }
    
    /*  
     *  This method is called when the game starts
     */
    public void LeftStartingTile()
    {
        timer.Show();
        timer.StartTiming();        
    }


                /* --------------- STATUS: PLAYER WON, PLAYER REACHED A FINISH-TILE ---------------  */
    public void Won()
    {
        timer.StopTiming();
        timer.ShowLastFinishTime();        
        SceneTransitionValues.time = timer.GetLastFinishTime();

        if(timer.IsNewBestTime())
        {
            SceneTransitionValues.newRecord = true;
        }

        // Debug.Log("Finish time: " + timer.GetLastFinishTime());

        if(settings.IsRestartingInsteadOfMenu())
        {
            StopMovement();
            GoToSpawnPosition(lastSpawnPosition, lastSpawnOffset, false);
        }
        else
        {            
            SceneManager.LoadScene("1_Scenes/_Menus/WinScreen");
        }        
    }


                /* --------------- STATUS: PLAYER LOST, PLAYER MET A LOSE CONDITION ---------------  */
    public void Lost()
    {
        Debug.Log("Time at loosing: " + timer.GetCurrentTime() + " || Position at loosing: " + transform.position);
        
        if(settings.IsRestartingInsteadOfMenu())
        {
            StopMovement();
            GoToSpawnPosition(lastSpawnPosition, lastSpawnOffset, false);
        }
        else
        {
            SceneManager.LoadScene("1_Scenes/_Menus/LoseScreen");
        }
    }
    
            /* --------------- STATUS: GAME PAUSED ---------------  */

    public void GamePaused()
    {
        if (settings.IsIntroductionScreen()) // check if the level has a introduction screen to show
        {
            tutorialManager.gameObject.SetActive(false);
        }
        accelerometerInformation.SetActive(false);

        if(distractionAtCanvas) distractionAtCanvas.SetActive(false);
        
        if(Game.hasStarted && !Game.isPaused) // just in case someone hits more times the pause button in a row, before the game was unpaused
        {
            rememberVelocity = rb.velocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            wasControlOn = controlOn;
            // DeactivatePlayerControls();
            timer.Pause();
            timer.Disappear();
        }
        else if(!Game.hasStarted)
        {
            skipButton.gameObject.SetActive(false);
        }
        Game.isPaused = true;
    }
            
    public void GameUnpaused()
    {
        if(Game.hasStarted)
        {
            StartCoroutine(UnpauseStopwatch(3));
            
        }
        else
        {
            skipButton.gameObject.SetActive(true);
        }
        
    }

    private IEnumerator UnpauseStopwatch(float seconds)
    {
        timer.SetStopWatch(seconds);
        timer.Show();

        skipButton.gameObject.SetActive(true);
        skipButton.Reset();
           
        while (!timer.IsStopTimeOver() && !skipButton.IsButtonPressed())
        {
            yield return new WaitForSeconds(0.00001f);
        }        

        if (settings.IsIntroductionScreen()) // check if the level has a introduction screen to show
        {
            tutorialManager.gameObject.SetActive(true);
        }

        skipButton.gameObject.SetActive(false);
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = rememberVelocity;

        // if(wasControlOn)
        // {
        //     ActivatePlayerControls();
        // }

        if(distractionAtCanvas) distractionAtCanvas.SetActive(true);
        accelerometerInformation.SetActive(false);

        if(Game.hasStarted && !occupiedTile.GetHexagon().IsStartingTile())
        {
            timer.Unpause();
        }
        else
        {
            timer.Disappear();
        }

        Game.isPaused = false;
    }
    

    /* ------------------------------ UPDATING AND WAITING FOR INPUT METHODS ------------------------------  */

    /*       
     *  So far just testing stuff
    **/
    // void FixedUpdate()
    // {
    //     // Save current position, not used yet
    //     positions.Add(transform.position);

    //     // Start ghost/replay --- JUST A TEST SO FAR ---
    //     if(Input.GetKeyDown(KeyCode.R))
    //     {                     
    //         transform.position = positions[replayPositionCounter];
    //         replayPositionCounter++;
    //     }
    // }


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
            occupiedTile = currentTile;         // Save the current tile
            currentTile.GotOccupied(this);      // Tell the currentTile, that this player stands on it            
            AnalyseOccupiedHexagon(currentTile.GetComponent<Hexagon>());            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        GameObject hexagonObject = collision.gameObject;

        if(hexagonObject.tag == "Tile")
        {
            HexagonBehaviour leftTile = hexagonObject.GetComponent<HexagonBehaviour>();

            leftTile.GotUnoccupied(this);   // Tell the former occupiedTile, that this ball left
            AnalyseUnoccupiedHexagon(occupiedTile.GetComponent<Hexagon>());
        }
    }


    /*  
     *  The player has to check for some specific hexagon types in order to decide what to do next, e. g. winning tiles have to change the state of the player
    **/
    private void AnalyseOccupiedHexagon(Hexagon hexagon)
    {        
        if(hexagon.IsStandardTile() && settings.DoesStandardTilesMeansLosing())
        {
            Lost();
        }

        if (settings.IsIntroductionScreen() && !hexagon.IsStandardTile() && !hexagon.IsStartingTile())
        {
            GetComponentInChildren<TutorialManager>().CheckForNonStandardTiles(hexagon, tiles, this);
        }
    }

    private void AnalyseUnoccupiedHexagon(Hexagon hexagon)
    {
        // Nothing to do yet
    }


    /* 
     *  This is constantly checking if a lose condition (ball fell to deep) has met
     *  It's packed in a coroutine, so it is not called every single frame -> saves performance
     */
    private IEnumerator CheckLoseCondition()
    {        
        // Lose condition through falling
        for(;;)
        {
            if(loseHeight > transform.position.y)
            {
                Lost();
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
        GoToSpawnPosition(lastSpawnPosition, lastSpawnOffset, true);
    }

    /*  
     *  Let the player spawn above the desired tile
    **/
    public void GoToSpawnPosition(Hexagon spawnTile, Vector3 spawnOffset, bool saveSpawn)
    {        
        if(spawnTile != null)
        {
            transform.position = spawnTile.transform.position + spawnOffset;

            if(saveSpawn)
            {
                lastSpawnPosition = spawnTile;
                lastSpawnOffset = spawnOffset;
            }            
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
        // Debug.Log("stopped control: " + Time.fixedTime);
    }

    public void ReverseMovement()
    {
        rb.velocity =  -rb.velocity;
        rb.angularVelocity = -rb.angularVelocity;
    }

    /*
     *  Deactivates the player attached scripts "Ball" and "AccelerometerMovement". Hence all the effects and manipulations caused by them will be absent.
    **/
    public void DeactivatePlayerControls()
    {
        if(GetComponent<BallControls>()) GetComponent<BallControls>().enabled = false;
        if(GetComponent<ControlsBallFromBehind>()) GetComponent<ControlsBallFromBehind>().enabled = false;
        if(GetComponent<AccelorometerMovement>()) GetComponent<AccelorometerMovement>().enabled = false;
        // Debug.Log("lost control: " + Time.fixedTime);
        controlOn = false;
    }


    /*
     * Activates the player attached scripts "Ball" and "AccelerometerMovement"
     **/
    public void ActivatePlayerControls()
    {
        if(GetComponent<BallControls>()) GetComponent<BallControls>().enabled = true;
        if(GetComponent<ControlsBallFromBehind>()) GetComponent<ControlsBallFromBehind>().enabled = true;
        if(GetComponent<AccelorometerMovement>()) GetComponent<AccelorometerMovement>().enabled = true;
        controlOn = true;
    }


    /* ------------------------------ GETTER METHODS ------------------------------  */
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public Camera GetPlayerCamera()
    {
        return Camera.main; // TO-DO for multiplayer: it couldn't be the main camera
    }


    /* ------------------------------ SETTER METHODS ------------------------------  */
    public void SetDistractionOnCanvas(GameObject distraction)
    {
        distractionAtCanvas = distraction;
    }

    public void RemoveDistractionOnCanvas()
    {
        distractionAtCanvas = null;        
    }

} // CLASS END