using UnityEngine;
using TMPro;
/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerField;

    
    private HexagonBehaviour occupiedTile;    
    private Timer timer;
    private Vector3 pos;


    /* ------------------------------ STANDARD METHODS BEGINN ------------------------------  */

    public void GetStarted()
    {
        timer = this.GetComponentInChildren<Timer>();
    }

    /*  
     *  Everythign which has to do with the players movement 
    **/
    void FixedUpdate()
    {   
        // Set timer
        timerField.text = timer.TimeToString(timer.GetCurrentTime());

        // Save current position, not used yet
        pos = transform.position;        
    }

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
                occupiedTile = currentTile;         // Save the current tile

                AnalyseHexagon(occupiedTile.GetComponent<Hexagon>());
            }            
        }
    }



    /* ------------------------------ BEHAVIOUR METHODS BEGINN ------------------------------  */

    /*  
     *  Let the player spawn above the desired tile
    **/
    private void AnalyseHexagon(Hexagon hexagon)
    {

        if(hexagon.IsStartingTile())
        {
            timer.StartTiming();
            Debug.Log("Timer started/reseted");
            Debug.Log("Record to beat: " + timer.GetBestTime());
        }
        else if(hexagon.IsWinningTile())
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
    }

    
    public void GoToSpawnPosition(Hexagon spawnTile)
    {
        float distanceAboveTile = 1f; // Should go later to a central place for all settings
        gameObject.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + distanceAboveTile, spawnTile.transform.position.z);
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