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

    private int playerNumber;
    private float loseHeight = -10;
    private int replayPositionCounter = 0;
    private int numberOfCheckpoints;
    
    


    /* ------------------------------ METHODS FOR DIFFERENT STATES BEGINN ------------------------------  */



                    /* --------------- STATUS: SCENE LOADED, PLAYER GETS PREPARED ---------------  */
    /*  
     *  Preparing the ball by saving some of it's components or getting values from the map
     */

    public void GetStarted(int playerNumber, int numberOfCheckpoints, Dictionary<int, List<Hexagon>> checkpointTiles, bool introductionScreen)
    {
        rb = GetComponent<Rigidbody>();
        timer = this.GetComponentInChildren<Timer>();
        this.playerNumber = playerNumber;

        GameObject loseTile = GameObject.Find("Map/LoseHeight");
        loseHeight = loseTile.transform.position.y;

        this.numberOfCheckpoints = numberOfCheckpoints;
        this.checkpointTiles = checkpointTiles;

        if(introductionScreen)
        {
            ShowIntroductionScreen();
        }

        if(numberOfCheckpoints > 0)
        {
            StartCoroutine(PlayerChoosesCheckpoints());
        }

        StartCoroutine(Introduction());
    }


    private void ShowIntroductionScreen()
    {
        // do something
    }

    /*
     *  This is just a basic idea how the choosing of checkpoints could look like
     */
    IEnumerator PlayerChoosesCheckpoints()
    {

        // maybe this way: https://forum.unity.com/threads/detecting-mouse-click-on-object.19450/
        // for(int i = 0; i < numberOfCheckpoints; i++)
        // {
        //     checkpointTiles[i].Add(clickedHexagon);
        // }
        
        // playerFinishedChoosingCheckpoints = true;
        yield return new WaitForSeconds(0.2f);
    }



                    /* --------------- STATUS: INTRODUCTION, PLAYER HAS TO WAIT ---------------  */
    /*
     *  All the colours of the non-standard tiles will be shown
     *  When all colours have faded, then the game starts
     */
    IEnumerator Introduction()
    {
        GameObject tiles = GameObject.Find("Map/Tiles");
        TileColorsIntroduction tileColorsIntroduction = tiles.GetComponent<TileColorsIntroduction>();
        tileColorsIntroduction.DisplayTiles();
        
        // Just wait for the tiles to finish 
        while(!tileColorsIntroduction.IsFinished())
        {
            yield return new WaitForSeconds(0.2f);
        }
        GameStarts();
    }

                /* --------------- STATUS: GAME STARTED, PLAYER CAN DO SOMETHING ---------------  */
    /*  
     *  This method is called when the game starts
     *  The player gets its controls and the timer will show up
     */
    private void GameStarts()
    {
        ActivatePlayerControls();
        timer.Show();
        StartCoroutine(CheckLoseCondition());
    }

    /*  
     *  This method is called when the game starts
     */
    private void PlayerTouchedStartingTile()
    {
        timer.StartTiming();
        Debug.Log("Timer started/reseted");
        Debug.Log("Record to beat: " + timer.GetBestTime());
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
        GameObject tile = collision.gameObject;

        if(tile.tag == "Tile")
        {
            HexagonBehaviour currentTile = tile.GetComponent<HexagonBehaviour>();
        
            if(occupiedTile != currentTile)         // Check if the former occupiedTile has changed   
            {
                if(occupiedTile != null)            // Prevent a NullReferenceException
                {
                    occupiedTile.GotUnoccupied(this);   // Tell the former occupiedTile, that this ball left
                }

                currentTile.GotOccupied(this);          // Tell the currentTile, that this player stands on it

                if(currentTile != null)                 // in case it is a crackableTile, it could be gone already
                {                    
                    occupiedTile = currentTile;         // Save the current tile
                }

                AnalyseHexagon(occupiedTile.GetComponent<Hexagon>());
            }            
        }
    }


    /*  
     *  The player has to check for some specific hexagon types in order to decide what to do next, e. g. starting or winning tiles have to change the state of the player
    **/
    private void AnalyseHexagon(Hexagon hexagon)
    {

        if(hexagon.IsStartingTile())
        {
            PlayerTouchedStartingTile();
        }
        else if(hexagon.IsWinningTile())
        {
            PlayerWon();
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