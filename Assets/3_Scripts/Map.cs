using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    
    Tiles tiles;
    Players players;

    
    void Start()
    {
        CreateTiles();
        CreatePlayers(1);        
    }

    void CreateTiles()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted();
        GameObject mapGenerator = GameObject.Find("/MapGenerator");
        mapGenerator.SetActive(false);
    }

    void CreatePlayers(int numberOfPlayers)
    {
        players = this.transform.GetComponentInChildren<Players>();
        players.GetStarted(numberOfPlayers, tiles.GetStartingTiles());
    }


/* ------------------------------ EDITOR MODE METHODS ------------------------------  */
    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }

/* ------------------------------ TO-MOVE ONCE STATE_MACHINE IS USED ------------------------------  */

    /*  
     *  Deactivates the player attached scripts "Ball" and "AccelerometerMovement". Hence all the effects and manipulations caused by them will be absent
    **/
    void DeactivatePlayerControls(GameObject player)
    {
            player.GetComponent<Ball>().enabled = false;
            player.GetComponent<AccelorometerMovement>().enabled = false;
            
    }

    /*
     * Activates the player attached scripts "Ball" and "AccelerometerMovement"
    **/
    void ActivatePlayerControls(GameObject player)
    {
        player.GetComponent<Ball>().enabled = true;
        player.GetComponent<AccelorometerMovement>().enabled = true;
    }

} // END OF CLASS
