using UnityEngine;
using TMPro;
/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerField;
    [SerializeField] float speed = 1000.0f;
    private HexagonBehaviour occupiedTile;
    
    private Timer timer;
    private Vector3 pos;


    /* ------------------------------ STANDARD METHODS BEGINN ------------------------------  */

    public void GetStarted()
    {
        timer = new Timer();
        
    }

    /*  
     *  This is the place, where the player gets controlled 
    **/
    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        pos = transform.position;

        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));        
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


    /* ------------------------------ GETTER METHODS BEGINN ------------------------------  */
        
    public Vector3 GetPos()
    {
        return pos; // current position in world
    }


    /* ------------------------------ BEHAVIOUR METHODS BEGINN ------------------------------  */

    /*  
     *  Let the player spawn above the desired tile
    **/


    public void GoToSpawnPosition(Hexagon spawnTile)
    {
        float distanceAboveTile = 1f; // Should go later to a central place for all settings
        gameObject.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + distanceAboveTile, spawnTile.transform.position.z);
    }

    private void AnalyseHexagon(Hexagon hexagon)
    {

        if(hexagon.IsStartingTile())
        {
            
        }
    }
    
} // CLASS END